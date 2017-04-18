using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.MvxPlugins.Fetcher.Services;
using artm.MvxPlugins.Fetcher.Droid.Services;

namespace artm.MvxPlugins.Fetcher.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.ConstructAndRegisterSingleton<IFetcherWebService, FetcherWebService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherRepositoryService, FetcherRepositoryService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherService, FetcherService>();
        }
    }
}