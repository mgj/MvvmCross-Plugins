using artm.MvxPlugins.Fetcher.Services;
using Square.OkHttp;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using artm.MvxPlugins.Fetcher.Models;

namespace artm.MvxPlugins.Fetcher.Droid.Services
{
    public class FetcherService : FetcherServiceBase
    {
        private OkHttpClient _client;

        public FetcherService(IFetcherRepositoryService repositoryService) : base(repositoryService)
        {
        }

        protected OkHttpClient Client
        {
            get
            {
                if (_client == null) _client = new OkHttpClient();
                return _client;
            }
        }

        protected override FetcherWebResponse DoPlatformWebRequest(Uri uri)
        {
            var request = new Request.Builder().Url(uri.OriginalString).Build();
            var response = Client.NewCall(request).Execute();
            return new FetcherWebResponse()
            {
                IsSuccess = response.IsSuccessful,
                Body = response.Body().String()
            };
        }
    }
}