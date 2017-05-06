using artm.MvxPlugins.Logger.Services;
using artm.MvxPlugins.Logger.Touch.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace artm.MvxPlugins.Logger.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.ConstructAndRegisterSingleton<ILoggerService, LoggerService>();
        }
    }
}