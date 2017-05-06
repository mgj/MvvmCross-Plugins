using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.Fetcher.Droid.Services;
using artm.Fetcher.Core.Services;

namespace artm.MvxPlugins.Fetcher.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            IFetcherRepositoryStoragePathService path = new FetcherRepositoryStoragePathService();
            IFetcherRepositoryService repository = new FetcherRepositoryService(path);
            IFetcherWebService web = new FetcherWebService();
            
            Mvx.LazyConstructAndRegisterSingleton<IFetcherService>(() => new FetcherService(web, repository));
            Mvx.Resolve<IFetcherService>();
        }
    }
}