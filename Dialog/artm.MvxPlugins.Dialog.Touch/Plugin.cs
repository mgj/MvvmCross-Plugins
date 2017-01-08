using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Dialog.Touch.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace artm.MvxPlugins.Dialog.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.ConstructAndRegisterSingleton<IDialogService, DialogService>();
        }
    }
}