﻿using artm.MvxPlugins.Fetcher.Entities;
using ModernHttpClient;
using Polly;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Services
{
    public class FetcherService : IFetcherService
    {
        public const long CACHE_FRESHNESS_THRESHOLD = TimeSpan.TicksPerDay;
        //private readonly IDreamsLogService _log;

        public FetcherService()
        {
            //_log = logService;
        }

        public async Task<UrlCacheInfo> Fetch(Uri url)
        {
            return await Fetch(url, CACHE_FRESHNESS_THRESHOLD);
        }

        public async Task<UrlCacheInfo> Fetch(Uri uri, long freshnessTreshold)
        {
            //_log.Log("Fetching for uri: " + uri.OriginalString);
            var realm = Realm.GetInstance();

            var needle = uri.OriginalString;
            var cacheHit = realm.All<UrlCacheInfo>().Where(x => x.Url == needle);
            if (cacheHit.Count() != 0)
            {
                // Hit
                //_log.Log("Cache hit");
                var hero = cacheHit.First();
                realm.Write(() => hero.LastAccessed = DateTime.Now.Ticks);

                if (ShouldInvalidate(hero, freshnessTreshold))
                {
                    // Cache needs refreshing
                    //_log.Log("Refreshing cache");
                    await UpdateUrl(uri, hero);
                }

                return hero;
            }
            else
            {
                // Nothing in cache, get it fresh
                return await InsertUrl(uri);
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

        private bool ShouldInvalidate(UrlCacheInfo hero, long freshnessTreshold)
        {
            return TimeSpanBetween(hero.LastUpdated, DateTime.Now.Ticks) > freshnessTreshold;
        }

        private async Task UpdateUrl(Uri uri, UrlCacheInfo hero)
        {
            var realm = Realm.GetInstance();
            var response = await FetchFromWeb(uri);
            if (string.IsNullOrEmpty(response))
            {
                return;
            }

            var timestamp = DateTime.Now.Ticks;
            realm.Write(() =>
            {
                hero.Response = response;
                hero.Url = uri.OriginalString;
                hero.LastAccessed = timestamp;
                hero.LastUpdated = timestamp;
            });
        }

        private async Task<UrlCacheInfo> InsertUrl(Uri uri)
        {
            var realm = Realm.GetInstance();
            UrlCacheInfo hero = null;
            var response = await FetchFromWeb(uri);
            if (string.IsNullOrEmpty(response))
            {
                return hero;
            }

            var timestamp = DateTime.Now.Ticks;
            realm.Write(() =>
            {
                hero = realm.CreateObject<UrlCacheInfo>();
                hero.Response = response;
                hero.Url = uri.OriginalString;
                hero.Created = timestamp;
                hero.LastAccessed = timestamp;
                hero.LastUpdated = timestamp;
            });

            //_log.Log("New url cached: " + uri.OriginalString);

            return hero;
        }

        private long TimeSpanBetween(long lastUpdated, long ticks)
        {
            return Math.Abs(lastUpdated - ticks);
        }

        private async Task<HttpResponseMessage> DoWebRequest(Uri uri)
        {
            var client = new HttpClient(new NativeMessageHandler());
            return await client.GetAsync(uri);
        }
    }
}
