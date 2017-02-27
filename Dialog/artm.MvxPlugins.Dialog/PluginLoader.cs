using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace artm.MvxPlugins.Dialog
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

            var manager = Mvx.Resolve<IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<PluginLoader>();
        }
    }
}
