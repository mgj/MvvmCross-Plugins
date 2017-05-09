using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Dialog.Touch.Services;
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

            //IFetcherRepositoryStoragePathService path = new FetcherRepositoryStoragePathService();
            //IFetcherRepositoryService repository = new FetcherRepositoryService(path);
            //IFetcherWebService web = new FetcherWebService();

            //Mvx.LazyConstructAndRegisterSingleton<IFetcherService>(() => new FetcherService(web, repository));

        }
    }
}
