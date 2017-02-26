using artm.MvxPlugins.Dialog.Droid.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Dialog.Services
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

            var result = true;
            for (int i = 0; i < hero.Length; i++)
            {
                if (result == false) break;

                for (int j = 0; j < other.Length; j++)
                {
                    result = hero[i].Title == other[j].Title;
                    if(result != false)
                    {
                        result = hero[i].Description == other[j].Description;
                    }
                }
            }

            return result;
        }
    }
}
