using artm.MvxPlugins.Fetcher.Services;
using artm.MvxPlugins.Logger.Services;
using System;
using System.Threading.Tasks;
using artm.MvxPlugins.Fetcher.Models;

namespace artm.MvxPlugins.Fetcher.Tests.Services.Calculator
{
    public class FetcherServiceMock : FetcherServiceBase
    {
        public FetcherServiceMock(IFetcherRepositoryService repository) 
            : base(repository)
        {
        }

        public string FetchFromWebResponse
        {
            get;
            set;
        }

        protected override FetcherWebResponse DoPlatformWebRequest(Uri uri)
        {
            var result = new FetcherWebResponse()
            {
                IsSuccess = true,
                Body = "myBody"
            };

            return result;
        }

        protected override Task<string> FetchFromWeb(Uri uri)
        {
            return Task.FromResult(FetchFromWebResponse);
        }
    }
}
