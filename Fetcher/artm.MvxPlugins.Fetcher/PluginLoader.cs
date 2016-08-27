using artm.MvxPlugins.Fetcher.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher
{
    public class PluginLoader : IMvxPluginLoader
    {
        public static readonly PluginLoader Instance = new PluginLoader();

        public PluginLoader()
        {
            Mvx.ConstructAndRegisterSingleton<IFetcherService, FetcherService>();
        }

        public void EnsureLoaded()
        {

            Mvx.ConstructAndRegisterSingleton<IFetcherService, FetcherService>();
        }
    }
}
