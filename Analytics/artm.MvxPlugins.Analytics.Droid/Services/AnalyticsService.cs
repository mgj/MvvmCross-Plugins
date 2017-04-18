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
using artm.MvxPlugins.Analytics.Services;
using Firebase.Analytics;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Platform;

namespace artm.MvxPlugins.Analytics.Droid.Services
{
    public class AnalyticsService : AnalyticsServiceBase
    {
        private readonly FirebaseAnalytics _analytics;

        public AnalyticsService()
        {
            _analytics = FirebaseAnalytics.GetInstance(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
        }

        public void Log(string id, string name)
        {
            var bundle = new Bundle();
            bundle.PutString(FirebaseAnalytics.Param.ItemId, id);
            bundle.PutString(FirebaseAnalytics.Param.ItemName, name);
            bundle.PutString(FirebaseAnalytics.Param.ContentType, "image");
            _analytics.LogEvent(FirebaseAnalytics.Event.SelectContent, bundle);
        }
    }
}