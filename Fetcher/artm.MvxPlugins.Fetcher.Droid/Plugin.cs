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