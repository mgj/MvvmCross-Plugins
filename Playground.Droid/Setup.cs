using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Dialog.Droid.Services;
using artm.MvxPlugins.Logger.Services;
using artm.MvxPlugins.Logger.Droid.Services;

namespace Playground.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            Mvx.LazyConstructAndRegisterSingleton<ILoggerService>(() => new LoggerService(ApplicationContext));
            Mvx.ConstructAndRegisterSingleton<IDialogService, DialogService>();
        }
    }
}
