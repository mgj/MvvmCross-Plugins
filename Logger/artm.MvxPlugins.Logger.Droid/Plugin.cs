using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.MvxPlugins.Logger.Droid.Services;
using MvvmCross.Platform.Droid;
using artm.MvxPlugins.Logger.Services;

namespace artm.MvxPlugins.Logger.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<ILoggerService>(() => new LoggerService(Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext));
        }
    }
}