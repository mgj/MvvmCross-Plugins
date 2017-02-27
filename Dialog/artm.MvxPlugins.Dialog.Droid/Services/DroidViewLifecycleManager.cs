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
using MvvmCross.Platform.Droid.Platform;
using System.Collections.Concurrent;

namespace artm.MvxPlugins.Dialog.Droid.Services
{
    /// <summary>
    /// This class implements IActivityLifecycleCallbacks and is registered on application startup to 
    /// listen for activity lifecycle events - which it then uses to track the Activities, including which Activity is Current.
    /// </summary>
    public class DroidViewLifecycleManager : Java.Lang.Object, global::Android.App.Application.IActivityLifecycleCallbacks, IMvxAndroidCurrentTopActivity
    {

        public event EventHandler<ViewLifetimeEventArgs> LifetimeChanged;

        public DroidViewLifecycleManager()
        {

        }

        private ConcurrentDictionary<string, ActivityInfo> _Activities = new ConcurrentDictionary<string, ActivityInfo>();

        protected string GetActivityName(Activity activity)
        {
            return activity.Class.SimpleName;
        }

        public Activity GetCurrentActivity()
        {
            if (_Activities.Count > 0)
            {
                var e = _Activities.GetEnumerator();
                while (e.MoveNext())
                {
                    var current = e.Current;
                    if (current.Value.IsCurrent)
                    {
                        return current.Value.Activity;
                    }
                }

                // var key = _Activities..;
            }

            return null;
        }

        #region IActivityLifecycleCallbacks

        public void OnActivityCreated(Activity activity, global::Android.OS.Bundle savedInstanceState)
        {
            _Activities.GetOrAdd(GetActivityName(activity), new ActivityInfo() { Activity = activity, IsCurrent = true });
            OnLifetimeChanged(activity, LifecycleEventType.Created);
            // throw new NotImplementedException();
        }

        public void OnActivityDestroyed(Activity activity)
        {
            ActivityInfo removed;
            _Activities.TryRemove(GetActivityName(activity), out removed);
            OnLifetimeChanged(activity, LifecycleEventType.Destroyed);
        }

        public void OnActivityPaused(Activity activity)
        {
            ActivityInfo forAdd = new ActivityInfo { Activity = activity, IsCurrent = false };
            _Activities.AddOrUpdate(GetActivityName(activity), forAdd, (key, existing) =>
            {
                existing.Activity = activity;
                existing.IsCurrent = false;
                return existing;
            });
            OnLifetimeChanged(activity, LifecycleEventType.Paused);
        }

        public void OnActivityResumed(Activity activity)
        {
            ActivityInfo forAdd = new ActivityInfo { Activity = activity, IsCurrent = false };
            _Activities.AddOrUpdate(GetActivityName(activity), forAdd, (key, existing) =>
            {
                existing.Activity = activity;
                existing.IsCurrent = true;
                return existing;
            });
            OnLifetimeChanged(activity, LifecycleEventType.Resumed);
            //lock (_Lock)
            //{
            //    _CurrentActivity = activity;
            //}
        }

        public void OnActivitySaveInstanceState(Activity activity, global::Android.OS.Bundle outState)
        {
            // throw new NotImplementedException();
        }

        public void OnActivityStarted(Activity activity)
        {
            ActivityInfo forAdd = new ActivityInfo { Activity = activity, IsCurrent = false };
            _Activities.AddOrUpdate(GetActivityName(activity), forAdd, (key, existing) =>
            {
                existing.Activity = activity;
                existing.IsCurrent = true;
                return existing;
            });
            OnLifetimeChanged(activity, LifecycleEventType.Started);
        }

        public void OnActivityStopped(Activity activity)
        {
            ActivityInfo forAdd = new ActivityInfo { Activity = activity, IsCurrent = false };
            _Activities.AddOrUpdate(GetActivityName(activity), forAdd, (key, existing) =>
            {
                existing.Activity = activity;
                existing.IsCurrent = false;
                return existing;
            });
            OnLifetimeChanged(activity, LifecycleEventType.Stopped);
        }

        #endregion

        protected virtual void OnLifetimeChanged(Activity activity, LifecycleEventType eventType)
        {
            if (LifetimeChanged != null)
            {
                var args = new ViewLifetimeEventArgs(activity, eventType);
                LifetimeChanged(this, args);
            }
        }

        #region Nested Helper Class

        /// <summary>
        /// Used to store additional info along with an activity.
        /// </summary>
        private class ActivityInfo
        {
            public bool IsCurrent { get; set; }
            public Activity Activity { get; set; }
        }

        #endregion

        public Activity Activity
        {
            get { return GetCurrentActivity(); }
        }
    }
}