using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Dialog.Touch.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

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