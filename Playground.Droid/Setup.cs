using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Logger.Services;
using artm.MvxPlugins.Logger.Droid.Services;
using artm.MvxPlugins.Dialog.Droid.Services;
using artm.Fetcher.Core.Services;
using artm.Fetcher.Droid.Services;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

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

            Mvx.ConstructAndRegisterSingleton<IFetcherLoggerService, FetcherLoggerService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherWebService, FetcherWebService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherRepositoryStoragePathService, FetcherRepositoryStoragePathService>();
            Mvx.LazyConstructAndRegisterSingleton<IFetcherRepositoryService>(() => new FetcherRepositoryService(Mvx.Resolve<IFetcherLoggerService>(), () => CreateConnection(Mvx.Resolve<IFetcherRepositoryStoragePathService>())));
            Mvx.LazyConstructAndRegisterSingleton<IFetcherService>(() => new FetcherService(Mvx.Resolve<IFetcherWebService>(), Mvx.Resolve<IFetcherRepositoryService>(), Mvx.Resolve<IFetcherLoggerService>()));

            // Force construction of singletons
            var repository = Mvx.Resolve<IFetcherRepositoryService>() as FetcherRepositoryService;
            Mvx.Resolve<IFetcherService>();

            // Ensure database tables are created
            repository.Initialize().Wait();
            var debug = 42;
            //Task.Run(async () => await repository.Initialize());

        }

        private static SQLiteConnectionWithLock CreateConnection(IFetcherRepositoryStoragePathService path)
        {
            var str = new SQLiteConnectionString(path.GetPath(), false);
            return new SQLiteConnectionWithLock(new SQLitePlatformAndroidN(), str);
        }
    }
}
