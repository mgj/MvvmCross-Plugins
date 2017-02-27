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
using MvvmCross.Droid.Views;
using artm.MvxPlugins.Dialog.ViewModels;
using artm.MvxPlugins.Dialog.Services;
using MvvmCross.Platform;
using MvvmCross.Binding.BindingContext;

namespace artm.MvxPlugins.Dialog.Droid.Views
{
    [Activity]
    public class MultiChoiceListView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var view = new LinearLayout(this);
            var text = new TextView(this);
            text.Text = "MultiChoiceListView";
            view.AddView(text);
            SetContentView(Resource.Layout.activity_multichoice);

            var dialogService = Mvx.Resolve<IDialogService>();

            var debug = 42;
        }
    }
}