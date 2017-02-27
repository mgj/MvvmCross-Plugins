using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using artm.MvxPlugins.Dialog.Services;
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