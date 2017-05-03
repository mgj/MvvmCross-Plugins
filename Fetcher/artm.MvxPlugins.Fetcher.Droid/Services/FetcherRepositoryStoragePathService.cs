using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using artm.MvxPlugins.Fetcher.Services;

namespace artm.MvxPlugins.Fetcher.Droid.Services
{
    internal class FetcherRepositoryStoragePathService : IFetcherRepositoryStoragePathService
    {
        public string GetPath(string filename = "fetcher.db3")
        {
            return System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
        }
    }
}