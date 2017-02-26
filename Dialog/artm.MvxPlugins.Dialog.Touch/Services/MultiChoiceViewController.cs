using artm.MvxPlugins.Dialog.Droid.Services;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Bindings.SourceSteps;
using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace artm.MvxPlugins.Dialog.Touch.Services
{
    public class MultiChoiceViewController : UIViewController
    {
        private MvxTableViewSource _source;
        private UITableView _table;
        private readonly DialogServiceMultiItemsBundle _bundle;
        private readonly Action<List<int>> _onComplete;

        public MultiChoiceViewController(DialogServiceMultiItemsBundle bundle, Action<List<int>> onComplete)
        {
            if(string.IsNullOrEmpty(bundle.Title) == false)
            {
                this.Title = bundle.Title;
            }

            _bundle = bundle;
            _onComplete = onComplete;
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var okButton = PrepareOkButton();
            var cancelButton = PrepareCancelButton();
            var title = PrepareTitleLabel();
            PrepareTableView();
            PrepareSelectedRowItems();

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var statusbarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;
            var buttonHeight = 44;
            View.AddConstraints(

                title.AtTopOf(View, statusbarHeight),
                title.Height().EqualTo(buttonHeight),
                title.WithSameCenterX(View),

                _table.Below(title),
                _table.Above(okButton),
                _table.WithSameWidth(View),

                cancelButton.WithSameBottom(View),
                cancelButton.Below(_table),
                cancelButton.Height().EqualTo(buttonHeight),
                cancelButton.WithSameLeft(View).Plus(8),

                okButton.WithSameBottom(View),
                okButton.Below(_table),
                okButton.Height().EqualTo(buttonHeight),
                okButton.WithSameRight(View).Minus(8)
                );
        }

        

        private UILabel PrepareTitleLabel()
        {
            var label = new UILabel();
            label.Text = Title;

            Add(label);
            return label;
        }

        private void PrepareTableView()
        {
            _table = new UITableView();
            _table.AllowsMultipleSelection = true;
            PrepareTableViewSource();

            Add(_table);

            _table.ReloadData();
        }

        private void PrepareTableViewSource()
        {
            _source = new MultiChoiceTableViewSource(_table, _bundle.CheckedItems);
            var asStrings = _bundle.Items.Select(x => x.Title).ToArray();
            _source.ItemsSource = asStrings;
            _table.Source = _source;
        }

        private void PrepareSelectedRowItems()
        {
            for (int i = 0; i < _bundle.CheckedItems.Count(); i++)
            {
                var hero = _bundle.CheckedItems[i];
                var path = NSIndexPath.Create(0, i); // Section must be supplied. Assuming there is only 1 section.
                if (hero == true)
                {
                    _table.SelectRow(path, animated: false, scrollPosition: UITableViewScrollPosition.None);
                }
                else
                {
                    _table.DeselectRow(path, animated: false);
                }
            }
        }

        private UIButton PrepareCancelButton()
        {
            var button = UIButton.FromType(UIButtonType.System);
            button.SetTitle(_bundle.NegativeLabel, UIControlState.Normal);
            button.TouchUpInside += (sender, e) =>
            {
                PrepareSelectedRowItems(); // Reset the table items to whatever was originally passed in
                DismissViewController(true, null);
            };
            Add(button);
            return button;
        }

        private UIButton PrepareOkButton()
        {
            var button = UIButton.FromType(UIButtonType.System);
            button.SetTitle(_bundle.PositiveLabel, UIControlState.Normal);
            button.TouchUpInside += (sender, e) =>
            {
                DismissViewController(true, null);
            };
            Add(button);
            return button;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            if (_onComplete == null) return;

            _onComplete(GetSelectedRows());
        }

        public MvxTableViewSource TableSource
        {
            get
            {
                return _source;
            }
        }

        private List<int> GetSelectedRows()
        {
            var result = new List<int>();
            if (_table.IndexPathsForSelectedRows == null)
            {
                return result;
            }

            foreach (var row in _table.IndexPathsForSelectedRows)
            {
                result.Add(row.Row);
            }
            return result;
        }
    }
}