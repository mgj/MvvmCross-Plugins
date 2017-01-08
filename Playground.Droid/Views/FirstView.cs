using System;
using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using MvvmCross.Binding.BindingContext;
using Playground.Core.ViewModels;
using Android.Widget;

namespace Playground.Droid.Views
{
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);

            PrepareUI();
        }

        private void PrepareUI()
        {
            var bindingSet = this.CreateBindingSet<FirstView, FirstViewModel>();

            var button = FindViewById<Button>(Resource.Id.firstview_listdialog_button);
            bindingSet.Bind(button).To(vm => vm.ShowListCommand);


            bindingSet.Apply();
        }
    }
}
