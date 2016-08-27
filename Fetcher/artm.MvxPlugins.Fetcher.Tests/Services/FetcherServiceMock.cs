﻿using artm.MvxPlugins.Fetcher.Services;
using System;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Tests.Services.Calculator
{
    public class FetcherServiceMock : FetcherService
    {
        public FetcherServiceMock() 
            : base()
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
