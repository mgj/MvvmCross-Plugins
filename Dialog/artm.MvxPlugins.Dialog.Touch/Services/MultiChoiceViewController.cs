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
        private readonly IEnumerable _items;
        private MvxTableViewSource _source;
        private UITableView _table;
        private readonly bool[] _checkedItems;
        private readonly Action<List<int>> _onComplete;
        private readonly string _okLabel;
        private readonly string _cancelLabel;

        public MultiChoiceViewController(string title, string okLabel, string cancelLabel, IEnumerable items, bool[] checkedItems, Action<List<int>> onComplete)
        {
            if(string.IsNullOrEmpty(title) == false)
            {
                this.Title = title;
            }

            _okLabel = okLabel;
            _cancelLabel = cancelLabel;
            _items = items;
            _checkedItems = checkedItems;
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
            _source = new MyTableViewSource(_table, _checkedItems);
            _source.ItemsSource = _items;
            _table.Source = _source;
        }

        private void PrepareSelectedRowItems()
        {
            for (int i = 0; i < _checkedItems.Count(); i++)
            {
                var hero = _checkedItems[i];
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

        private class MyTableViewSource : MvxStandardTableViewSource
        {
            private readonly bool[] _checkedItems;

            public MyTableViewSource(UITableView table, bool[] checkedItems) : base(table)
            {
                // Make copy of array in order to be able to return the original checkedItems array (for the cancel button)
                _checkedItems = (bool[]) checkedItems.Clone();
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = base.GetCell(tableView, indexPath);
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;

                var hero = _checkedItems[indexPath.Row];
                if (hero == true)
                {
                    cell.Accessory = UITableViewCellAccessory.Checkmark;
                }
                else
                {
                    cell.Accessory = UITableViewCellAccessory.None;
                }

                return cell;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                _checkedItems[indexPath.Row] = true;

                var cell = tableView.CellAt(indexPath);
                if (cell == null) return;
                cell.Accessory = UITableViewCellAccessory.Checkmark;
            }

            public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
            {
                _checkedItems[indexPath.Row] = false;
                var cell = tableView.CellAt(indexPath);
                if (cell == null) return;
                cell.Accessory = UITableViewCellAccessory.None;
            }

            

        }

        private UIButton PrepareCancelButton()
        {
            var button = UIButton.FromType(UIButtonType.System);
            button.SetTitle(_cancelLabel, UIControlState.Normal);
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
            button.SetTitle(_okLabel, UIControlState.Normal);
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
            if(_onComplete == null)
            {
                return;
            }

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