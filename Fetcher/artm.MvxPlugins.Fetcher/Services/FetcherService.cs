using artm.MvxPlugins.Fetcher.Entities;
using artm.MvxPlugins.Logger.Services;
using ModernHttpClient;
using Polly;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Services
{
    public class FetcherService : IFetcherService
    {
        public readonly TimeSpan CACHE_FRESHNESS_THRESHOLD = TimeSpan.FromDays(1); // 1 day

        private readonly ILoggerService _log;
        private readonly IFetcherRepositoryService _repository;

        public FetcherService(ILoggerService logService, IFetcherRepositoryService repositoryService)
        {
            _log = logService;
            _repository = repositoryService;
        }

        public async Task<UrlCacheInfo> Fetch(Uri url)
        {
            return await Fetch(url, CACHE_FRESHNESS_THRESHOLD);
        }

        public async Task<UrlCacheInfo> Fetch(Uri uri, TimeSpan freshnessTreshold)
        {
            _log.Log("Fetching for uri: " + uri.OriginalString);

            var cacheHit = _repository.GetEntryForUrl(uri);
            if (cacheHit != null)
            {
                // Hit
                _log.Log("Cache hit");
                _repository.UpdateLastAccessed(cacheHit);
                if (ShouldInvalidate(cacheHit, freshnessTreshold))
                {
                    // Cache needs refreshing
                    _log.Log("Refreshing cache");
                    var response = await FetchFromWeb(uri);
                    _repository.UpdateUrl(uri, cacheHit, response);
                }

                return cacheHit;
            }
            else
            {
                // Nothing in cache, get it fresh
                var response = await FetchFromWeb(uri);
                return _repository.InsertUrl(uri, response);
            }
        }

        protected virtual async Task<string> FetchFromWeb(Uri uri)
        {
            var policy = Policy
                .HandleResult<HttpResponseMessage>(r => r.IsSuccessStatusCode == false)
                .WaitAndRetryAsync(5, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            var response = await policy.ExecuteAsync(() => DoWebRequest(uri));
            var etag = response.Headers.ETag;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }

        private bool ShouldInvalidate(UrlCacheInfo hero, TimeSpan freshnessTreshold)
        {
            var delta = hero.LastUpdated - DateTimeOffset.UtcNow;
            return delta > freshnessTreshold;
        }

        private async Task<HttpResponseMessage> DoWebRequest(Uri uri)
        {
            var client = new HttpClient(new NativeMessageHandler());
            return await client.GetAsync(uri);
        }
    }
}
