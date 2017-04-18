using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Dialog.Touch.Services;
using artm.MvxPlugins.Fetcher.Services;
using artm.MvxPlugins.Fetcher.Touch.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using UIKit;

namespace Playground.Touch
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }
        
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
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

            Mvx.ConstructAndRegisterSingleton<IDialogService, DialogService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherWebService, FetcherWebService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherRepositoryService, FetcherRepositoryService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherService, FetcherService>();

        }
    }
}
