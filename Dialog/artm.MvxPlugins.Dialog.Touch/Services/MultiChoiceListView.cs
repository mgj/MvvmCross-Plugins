using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace artm.MvxPlugins.Dialog.Touch.Services
{
    public class MultiChoiceListView : UITableViewController
    {
        private MvxStandardTableViewSource _source;
        private IEnumerable _items;

        // TODO: Add binding support for backing data
        public MultiChoiceListView(IEnumerable items)
        {
            _items = items;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _source = new MvxStandardTableViewSource(TableView);
            _source.ItemsSource = _items;

            TableView.Source = _source;
            TableView.ReloadData();
        }
    }
}