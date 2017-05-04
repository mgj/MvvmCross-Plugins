using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using artm.MvxPlugins.Fetcher.Services;

namespace artm.MvxPlugins.Fetcher.Touch.Services
{
    public class FetcherRepositoryStoragePathService : IFetcherRepositoryStoragePathService
    {
        public string GetPath(string filename = "fetcher.db3")
        {
            return System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
        }
    }
}