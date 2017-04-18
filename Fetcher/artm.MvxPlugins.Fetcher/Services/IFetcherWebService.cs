using artm.MvxPlugins.Fetcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Services
{
    public interface IFetcherWebService
    {
        FetcherWebResponse DoPlatformWebRequest(Uri uri);
    }
}
