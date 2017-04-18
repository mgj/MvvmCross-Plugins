using artm.MvxPlugins.Fetcher.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace artm.MvxPlugins.Fetcher
{
    public class PluginLoader : IMvxPluginLoader
    {
        public static readonly PluginLoader Instance = new PluginLoader();

        private bool _loaded = false;

        public void EnsureLoaded()
        {
            if (_loaded)
            {
                return;
            }

            _loaded = true;
            Mvx.ConstructAndRegisterSingleton<IFetcherService, FetcherServiceBase>();
        }
    }
}
