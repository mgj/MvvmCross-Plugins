
using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Playground.Droid.Views
{
    [Activity]
    public class SecondView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SecondView);
        }
    }
}