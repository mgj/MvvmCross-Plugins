using artm.MvxPlugins.Fetcher.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

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