using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.MvxPlugins.Logger.Droid.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using MvvmCross.Platform.Droid;

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