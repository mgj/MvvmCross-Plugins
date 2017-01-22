using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace artm.MvxPlugins.Dialog.Droid.Services
{
    public class DialogServiceMultiItemsBundle
    {
        private string _title;
        private string[] _items;
        private bool[] _checkedItems;
        private string _positiveLabel;

        public DialogServiceMultiItemsBundle(string title, string[] items, bool[] checkedItems, string positiveLabel = "Okay")
        {
            _title = title;
            _items = items;
            _checkedItems = checkedItems;
            _positiveLabel = positiveLabel;
        }

        public static bool SameValuesAs(DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
        {
            if (hero.Title.Equals(other.Title) && hero.PositiveLabel.Equals(other.PositiveLabel))
            {
                if (hero.Items.SequenceEqual(other.Items))
                {
                    if (hero.CheckedItems.SequenceEqual(other.CheckedItems))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public string[] Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public bool[] CheckedItems
        {
            get
            {
                return _checkedItems;
            }
            set
            {
                _checkedItems = value;
            }
        }

        public string PositiveLabel
        {
            get
            {
                return _positiveLabel;
            }
            set
            {
                _positiveLabel = PositiveLabel;
            }
        }
    }
}