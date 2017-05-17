using MvvmCross.Platform.Plugins;
using MvvmCross.Platform;
using artm.Fetcher.Core.Services;
using artm.Fetcher.Touch.Services;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

namespace artm.MvxPlugins.Fetcher.Touch
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.ConstructAndRegisterSingleton<IFetcherLoggerService, FetcherLoggerService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherWebService, FetcherWebService>();
            Mvx.ConstructAndRegisterSingleton<IFetcherRepositoryStoragePathService, FetcherRepositoryStoragePathService>();
            Mvx.LazyConstructAndRegisterSingleton<IFetcherRepositoryService>(() => new FetcherRepositoryService(() => CreateConnection(Mvx.Resolve<IFetcherRepositoryStoragePathService>())));
            Mvx.LazyConstructAndRegisterSingleton<IFetcherService>(() => new FetcherService(Mvx.Resolve<IFetcherWebService>(), Mvx.Resolve<IFetcherRepositoryService>(), Mvx.Resolve<IFetcherLoggerService>()));

            Mvx.Resolve<IFetcherRepositoryService>();
            Mvx.Resolve<IFetcherService>();
        }

        private static SQLiteConnectionWithLock CreateConnection(IFetcherRepositoryStoragePathService path)
        {
            var str = new SQLiteConnectionString(path.GetPath(), false);
            return new SQLiteConnectionWithLock(new SQLitePlatformIOS(), str);
        }
    }
}