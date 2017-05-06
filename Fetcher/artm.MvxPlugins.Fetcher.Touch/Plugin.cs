using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.Fetcher.Core.Services;
using artm.Fetcher.Touch.Services;

namespace artm.MvxPlugins.Fetcher.Touch
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