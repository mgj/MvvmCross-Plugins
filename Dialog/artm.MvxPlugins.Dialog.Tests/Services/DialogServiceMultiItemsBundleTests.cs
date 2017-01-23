using artm.MvxPlugins.Dialog.Droid.Services;
using artm.MvxPlugins.Dialog.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Dialog.Tests.Services
{
    [TestFixture]

    public class DialogServiceMultiItemsBundleTests
    {
        [Test]
        public void SameValuesAs_EqualInput_ReturnsTrue()
        {
            var sut = Factory();
            var sut2 = Factory();

            Assert.IsTrue(DialogServiceMultiItemsBundle.SameValuesAs(sut, sut2));
        }

        [Test]
        public void SameValueAs_UnEqualInput_ReturnsFalse()
        {
            var sut = Factory();
            var sut2 = Factory(new[] { "test", "test", "test" });

            Assert.IsFalse(DialogServiceMultiItemsBundle.SameValuesAs(sut, sut2));
        }

        private static DialogServiceMultiItemsBundle Factory()
        {
            var myItems = new[] { "a", "b", "c" };

            DialogServiceMultiItemsBundle sut = Factory(myItems);
            return sut;
        }

        private static DialogServiceMultiItemsBundle Factory(string[] myItems)
        {
            string _title = "title";
            string[] _items = myItems;
            bool[] _checkedItems = new[] { true, false, false };
            string _positiveLabel = "Okk";
            var sut = new DialogServiceMultiItemsBundle(_title, _items, _checkedItems, _positiveLabel);
            return sut;
        }
    }
}
