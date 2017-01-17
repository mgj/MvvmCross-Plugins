using Cirrious.FluentLayouts.Touch;
using Foundation;
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
        private bool[] _checkedItems;
        private readonly Action<List<int>> _onComplete;
        private readonly string _dismissLabel;
        
        public MultiChoiceViewController(string title, string dismissLabel, IEnumerable items, bool[] checkedItems, Action<List<int>> onComplete)
        {
            if(string.IsNullOrEmpty(title) == false)
            {
                this.Title = title;
            }

            _dismissLabel = dismissLabel;
            _items = items;
            _checkedItems = checkedItems;
            _onComplete = onComplete;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var okayButton = PrepareOkayButton();
            PrepareTableView();
            var title = PrepareTitleLabel();

            for (int i = 0; i < _checkedItems.Count(); i++)
            {
                var hero = _checkedItems[i];
                if (hero == true)
                {
                    var path = NSIndexPath.Create(0, i); // Section must be supplied. Assuming there is only 1 section.
                    _table.SelectRow(path, animated: false, scrollPosition: UITableViewScrollPosition.None);
                }
            }

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var statusbarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;
            var buttonHeight = 44;
            View.AddConstraints(
                title.AtTopOf(View, statusbarHeight),
                title.Height().EqualTo(buttonHeight),
                title.WithSameLeft(View),

                okayButton.WithSameTop(title),
                okayButton.WithSameHeight(title),
                okayButton.WithSameRight(View),

                _table.Below(okayButton),
                _table.WithSameWidth(View),
                _table.WithSameBottom(View)
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
            _source = new MvxStandardTableViewSource(_table);
            _source.ItemsSource = _items;
            _table.Source = _source;

            _table.ReloadData();
            Add(_table);
        }

        private UIButton PrepareOkayButton()
        {
            var button = UIButton.FromType(UIButtonType.System);
            button.SetTitle(_dismissLabel, UIControlState.Normal);
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