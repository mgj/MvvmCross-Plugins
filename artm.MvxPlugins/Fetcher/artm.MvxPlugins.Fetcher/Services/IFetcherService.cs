using artm.MvxPlugins.Fetcher.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Services
{
    public interface IFetcherService
    {
        Task<UrlCacheInfo> Fetch(Uri url);
        Task<UrlCacheInfo> Fetch(Uri url, long freshnessTreshold);
    }
}
