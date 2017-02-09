using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Views;

namespace Playground.Droid
{
    [Activity(
        Label = "Playground.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
        }
    }
}
