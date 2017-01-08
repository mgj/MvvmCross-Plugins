using artm.MvxPlugins.Logger.Services;
using artm.MvxPlugins.Logger.Touch.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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