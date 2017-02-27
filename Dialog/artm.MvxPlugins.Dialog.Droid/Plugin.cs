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
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Dialog.Droid.Views;
using artm.MvxPlugins.Dialog.Droid.Services;

namespace artm.MvxPlugins.Dialog.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.ConstructAndRegisterSingleton<IDialogService, DialogService>();
        }
    }
}