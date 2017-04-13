using artm.MvxPlugins.Fetcher.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Services
{
    public interface IFetcherRepositoryService
    {
        UrlCacheInfo GetEntryForUrl(Uri url);

        void UpdateLastAccessed(UrlCacheInfo hero);

        void UpdateUrl(Uri uri, UrlCacheInfo hero, string response);

        UrlCacheInfo InsertUrl(Uri uri, string response);
    }
}
