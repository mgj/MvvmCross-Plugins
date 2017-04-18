using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.MvxPlugins.Fetcher.Services;
using artm.MvxPlugins.Fetcher.Touch.Services;

namespace artm.MvxPlugins.Fetcher.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.ConstructAndRegisterSingleton<IFetcherService, FetcherService>();
        }
    }
}