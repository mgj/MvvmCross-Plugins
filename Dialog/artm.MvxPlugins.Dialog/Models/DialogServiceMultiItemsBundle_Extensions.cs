using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Dialog.Models
{
    public static class DialogServiceMultiItemsBundle_Extensions
    {
        public static bool SameValuesAs(this DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
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

        public static bool SameCheckedItemsAs(this DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
        {
            if (hero == null || other == null) return false;

            return hero.CheckedItems.SequenceEqual(other.CheckedItems);
        }

        public static bool SameItemsAs(this DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
        {
            if (hero == null || other == null) return false;

            return hero.Items.AreSame(other.Items);
        }

        public static bool SameTitleAs(this DialogServiceMultiItemsBundle hero, DialogServiceMultiItemsBundle other)
        {
            if (hero == null || other == null) return false;

            return hero.Title.Equals(other.Title) && hero.PositiveLabel.Equals(other.PositiveLabel);
        }
    }
}
