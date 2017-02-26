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
using artm.MvxPlugins.Dialog.Services;
using MvvmCross.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Binding.BindingContext;

namespace artm.MvxPlugins.Dialog.Droid.Views
{
    [Activity]
    public class MultiChoiceDetailsView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var view = new LinearLayout(this);
            var text = new TextView(this);
            text.Text = "MMultiChoiceDetailsView";
            view.AddView(text);

            SetContentView(view);

            var bindingSet = this.CreateBindingSet<MultiChoiceDetailsView, MultiChoiceDetailsViewModel>();

            var dialogService = Mvx.Resolve<IDialogService>();

            var debug = 42;

            //var requestCode = Intent.GetIntExtra(MULTICHOICE_REQUESTCODE_NORMAL, 0);
            //var title = Intent.GetStringExtra(MULTICHOICE_TITLE);
            //var items = Intent.GetStringArrayExtra(MULTICHOICE_ITEMS);
            //var checkedItems = Intent.GetBooleanArrayExtra(MULTICHOICE_CHECKEDITEMS);
            //var positiveLabel = Intent.GetStringExtra(MULTICHOICE_POSITIVELABEL);
            //var negativeLabel = Intent.GetStringExtra(MULTICHOICE_NEGATIVELABEL);

            //var dialogBundle = new DialogServiceMultiItemsBundle(title, items, checkedItems, positiveLabel, negativeLabel);

            //var okButton = FindViewById<Button>(Resource.Id.button_ok);
            //var cancelButton = FindViewById<Button>(Resource.Id.button_cancel);
            //okButton.Text = dialogBundle.PositiveLabel; 
            //cancelButton.Text = dialogBundle.NegativeLabel;

            //var listview = FindViewById<MvvmCross.Binding.Droid.Views.MvxListView>(Resource.Id.listview);
            //listview.ItemsSource = dialogBundle.Items;
        }
    }
}