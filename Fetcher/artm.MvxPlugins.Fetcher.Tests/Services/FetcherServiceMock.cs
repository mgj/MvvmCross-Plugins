using artm.MvxPlugins.Fetcher.Services;
using System;
using System.Threading.Tasks;
using artm.MvxPlugins.Fetcher.Models;

namespace artm.MvxPlugins.Fetcher.Tests.Services.Calculator
{
    public class FetcherServiceMock : FetcherServiceBase
    {
        public FetcherServiceMock(IFetcherRepositoryService repository, IFetcherWebService webService) 
            : base(repository, webService)
        {
        }

        public string FetchFromWebResponse
        {
            get;
            set;
        }


        protected override Task<string> FetchFromWeb(Uri uri)
        {
            return Task.FromResult(FetchFromWebResponse);
        }
    }
}
