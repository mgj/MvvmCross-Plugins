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
            Mvx.ConstructAndRegisterSingleton<IFetcherService, FetcherService>();
        }
    }
}