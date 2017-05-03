using artm.MvxPlugins.Fetcher.Entities;
using System;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Services
{
    public interface IFetcherService
    {
        Task<IUrlCacheInfo> Fetch(Uri url);
        Task<IUrlCacheInfo> Fetch(Uri url, TimeSpan freshnessTreshold);
        void Preload(Uri url, string response);
    }
}
