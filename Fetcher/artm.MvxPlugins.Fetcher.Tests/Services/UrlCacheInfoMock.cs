using artm.MvxPlugins.Fetcher.Entities;
using System;

namespace artm.MvxPlugins.Fetcher.Tests.Services
{
    internal class UrlCacheInfoMock : IUrlCacheInfo
    {
        public string Response { get; set; }

        public string Url { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset LastAccessed { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
    }
}