using artm.MvxPlugins.Dialog.Models;
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

            Assert.IsTrue(sut.SameValuesAs(sut2));
        }

        [Test]
        public void SameValueAs_UnEqualInput_ReturnsFalse()
        {
            var sut = Factory();

            var myItems = new List<DialogServiceMultiItem>();
            myItems.Add(new DialogServiceMultiItem("test", "blabla blabla"));
            myItems.Add(new DialogServiceMultiItem("test", "blabla blabla"));
            myItems.Add(new DialogServiceMultiItem("test", "blabla blabla"));
            myItems.Add(new DialogServiceMultiItem("test", "blabla blabla"));
            var sut2 = Factory(myItems: myItems.ToArray());

            Assert.IsFalse(sut.SameValuesAs(sut2));
        }

        private static DialogServiceMultiItemsBundle Factory()
        {
            var myItems = new List<DialogServiceMultiItem>();
            myItems.Add(new DialogServiceMultiItem("a", "blabla blabla"));
            myItems.Add(new DialogServiceMultiItem("b", "blabla blabla"));
            myItems.Add(new DialogServiceMultiItem("c", "blabla blabla"));
            myItems.Add(new DialogServiceMultiItem("d", "blabla blabla"));

            DialogServiceMultiItemsBundle sut = Factory(myItems.ToArray());
            return sut;
        }

        private static DialogServiceMultiItemsBundle Factory(DialogServiceMultiItem[] myItems)
        {
            string _title = "title";
            DialogServiceMultiItem[] _items = myItems;
            bool[] _checkedItems = new[] { true, false, false };
            string _positiveLabel = "Okk";
            var sut = new DialogServiceMultiItemsBundle(_title, _items, _checkedItems, _positiveLabel);
            return sut;
        }
    }
}
