using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.Fetcher.Core.Services;
using artm.Fetcher.Touch.Services;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.ConstructAndRegisterSingleton<IFetcherLoggerService, FetcherLoggerService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherWebService, FetcherWebService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherRepositoryStoragePathService, FetcherRepositoryStoragePathService>();
            Mvx.LazyConstructAndRegisterSingleton<IFetcherRepositoryService>(() => new FetcherRepositoryService(Mvx.Resolve<IFetcherLoggerService>(), () => CreateConnection(Mvx.Resolve<IFetcherRepositoryStoragePathService>())));
            Mvx.ConstructAndRegisterSingleton<IFetcherService, FetcherService>();
            
            // Ensure database tables are created
            var repository = Mvx.Resolve<IFetcherRepositoryService>() as FetcherRepositoryService;
            Task.Run(async () => await repository.Initialize());
        }

        private static SQLiteConnectionWithLock CreateConnection(IFetcherRepositoryStoragePathService path)
        {
            var str = new SQLiteConnectionString(path.GetPath(), false);
            return new SQLiteConnectionWithLock(new SQLitePlatformIOS(), str);
        }
    }
}