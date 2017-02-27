using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Dialog.Models
{
    public static class DialogServiceMultiItems_Extensions
    {
        public static bool AreSame(this DialogServiceMultiItem hero, DialogServiceMultiItem other)
        {
            if (hero == null || other == null) return false;

            var sameTitle = hero.Title == other.Title;
            var sameDescription = hero.Description == other.Description;

            return (sameTitle && sameDescription);
        }

        public static bool AreSame(this DialogServiceMultiItem[] hero, DialogServiceMultiItem[] other)
        {
            if (hero == null || other == null) return false;
            if (hero.Length != other.Length) return false;

            var result = true;
            for (int i = 0; i < hero.Length; i++)
            {
                if (result == false) break;

                result = hero[i].Title == other[i].Title;
                if (result != false)
                {
                    result = hero[i].Description == other[i].Description;
                }
            }

            return result;
        }
    }
}
