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

namespace artm.MvxPlugins.Dialog.Droid.Services
{
    [Activity]
    public class MultiChoiceActivity : Activity
    {
        public const string MULTICHOICE_REQUESTCODE_NORMAL = "MultiChoiceActivity.MULTICHOICE_REQUESTCODE_NORMAL";
        public readonly string MULTICHOICE_CHECKEDITEMS = "MultiChoiceActivity.MULTICHOICE_CHECKEDITEMS";
        public readonly string MULTICHOICE_ITEMS = "MultiChoiceActivity.MULTICHOICE_ITEMS";
        public readonly string MULTICHOICE_NEGATIVELABEL = "MultiChoiceActivity.MULTICHOICE_NEGATIVELABEL";
        public readonly string MULTICHOICE_POSITIVELABEL = "MultiChoiceActivity.MULTICHOICE_POSITIVELABEL";
        public readonly string MULTICHOICE_TITLE = "MultiChoiceActivity.MULTICHOICE_TITLE";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_multichoice);

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