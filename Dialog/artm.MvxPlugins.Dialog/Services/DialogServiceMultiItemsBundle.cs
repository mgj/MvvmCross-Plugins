using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace artm.MvxPlugins.Dialog.Droid.Services
{
    public class DialogServiceMultiItemsBundle
    {
        public DialogServiceMultiItemsBundle(string title, string[] items, bool[] checkedItems, string positiveLabel = "Okay", string negativeLabel = "Cancel")
        {
            Title = title;
            Items = items;
            CheckedItems = checkedItems;
            PositiveLabel = positiveLabel;
            NegativeLabel = negativeLabel;
        }

        public bool[] CheckedItems { get; private set; }
        public string[] Items { get; private set; }
        public string NegativeLabel { get; private set; }
        public string PositiveLabel { get; private set; }
        public string Title { get; private set; }

        public static bool SameValuesAs(DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
        {
            if (hero == null || other == null) return false;
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
    }
}