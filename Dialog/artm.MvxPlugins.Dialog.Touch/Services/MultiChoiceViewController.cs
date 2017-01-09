using Cirrious.FluentLayouts.Touch;
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
        private readonly string _dismissTitle;

        public MultiChoiceViewController(IEnumerable items)
        {
        }

        public MultiChoiceViewController(string dismissTitle, IEnumerable items, bool[] checkedItems, Action<List<int>> onComplete)
        {
            _dismissTitle = dismissTitle;
            _items = items;
            _checkedItems = checkedItems;
            _onComplete = onComplete;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var button = UIButton.FromType(UIButtonType.System);
            button.SetTitle(_dismissTitle, UIControlState.Normal);
            button.TouchUpInside += (sender, e) => {
                DismissViewController(true, null);
            };
            Add(button);

            _table = new UITableView();
            _table.AllowsMultipleSelection = true;
            _source = new MvxStandardTableViewSource(_table);
            _source.ItemsSource = _items;
            _table.Source = _source;
            _table.ReloadData();
            Add(_table);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var statusbarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;
            View.AddConstraints(

                button.AtTopOf(View, statusbarHeight),
                button.WithSameWidth(View),
                button.Height().EqualTo(44),

                _table.Below(button),
                _table.WithSameWidth(View),
                _table.WithSameBottom(View)
                );
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
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
            foreach (var row in _table.IndexPathsForSelectedRows)
            {
                result.Add(row.Row);
            }
            return result;
        }
    }
}