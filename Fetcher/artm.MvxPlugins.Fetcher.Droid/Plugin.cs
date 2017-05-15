using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.Fetcher.Droid.Services;
using artm.Fetcher.Core.Services;
using artm.Fetcher.Core.Services.Tosser;

namespace artm.MvxPlugins.Fetcher.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.ConstructAndRegisterSingleton<IFetcherWebService, FetcherWebService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherRepositoryStoragePathService, FetcherRepositoryStoragePathService>();
            Mvx.LazyConstructAndRegisterSingleton<IFetcherRepositoryService>(() => new FetcherRepositoryService(Mvx.Resolve<IFetcherRepositoryStoragePathService>()));
            Mvx.LazyConstructAndRegisterSingleton<IFetcherService>(() => new FetcherService(Mvx.Resolve<IFetcherWebService>(), Mvx.Resolve<IFetcherRepositoryService>()));
            Mvx.LazyConstructAndRegisterSingleton<ITosserService>(() => new TosserService(Mvx.Resolve<IFetcherWebService>()));

            Mvx.Resolve<IFetcherRepositoryService>();
            Mvx.Resolve<IFetcherService>();
            Mvx.Resolve<ITosserService>();
        }
    }
}