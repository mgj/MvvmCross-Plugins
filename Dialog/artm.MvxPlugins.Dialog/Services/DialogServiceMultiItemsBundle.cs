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

            if (SameTitleAs(hero, other) && 
                SameItemsAs(hero, other) && 
                SameCheckedItemsAs(hero, other))
            {
                return true;
            }
            return false;
        }

        public static bool SameCheckedItemsAs(DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
        {
            if (hero == null || other == null) return false;

            return hero.CheckedItems.SequenceEqual(other.CheckedItems);
        }

        public static bool SameItemsAs(DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
        {
            if (hero == null || other == null) return false;

            return hero.Items.SequenceEqual(other.Items);
        }

        public static bool SameTitleAs(DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
        {
            if (hero == null || other == null) return false;

            return hero.Title.Equals(other.Title) && hero.PositiveLabel.Equals(other.PositiveLabel);
        }
    }
}