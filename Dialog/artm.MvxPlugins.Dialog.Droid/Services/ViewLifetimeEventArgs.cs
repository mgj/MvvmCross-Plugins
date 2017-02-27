using System;
using Android.App;

namespace artm.MvxPlugins.Dialog.Droid.Services
{
    public class ViewLifetimeEventArgs : EventArgs
    {
        private Activity activity;
        private LifecycleEventType eventType;

        public ViewLifetimeEventArgs(Activity activity, LifecycleEventType eventType)
        {
            this.activity = activity;
            this.eventType = eventType;
        }
    }
}