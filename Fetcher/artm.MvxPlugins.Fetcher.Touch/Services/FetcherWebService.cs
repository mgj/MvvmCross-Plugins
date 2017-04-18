
using System;
using artm.MvxPlugins.Fetcher.Models;
using artm.MvxPlugins.Fetcher.Services;
using Foundation;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Touch.Services
{
    public class FetcherWebService : IFetcherWebService
    {
        public FetcherWebResponse DoPlatformWebRequest(Uri uri)
        {
            var request = new NSMutableUrlRequest(uri);
            var session = NSUrlSession.SharedSession;
            request.HttpMethod = "GET";

            var tcs = new TaskCompletionSource<FetcherWebResponse>();

            var task = session.CreateDataTask(request,
                (data, response, error) =>
                {
                    var resp = response as NSHttpUrlResponse;
                    tcs.SetResult(new FetcherWebResponse()
                    {
                        IsSuccess = error == null,
                        Body = data.ToString()
                    });
                });

            task.Resume();
            return tcs.Task.Result;
        }
    }
}