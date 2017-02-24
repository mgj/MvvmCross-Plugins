using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace artm.MvxPlugins.Dialog.Touch.Services
{
    public class MultiChoiceViewControllerBundle
    {
        public MultiChoiceViewControllerBundle(string title, string okLabel, string cancelLabel, IEnumerable items, bool[] checkedItems, Action<List<int>> onComplete)
        {
            Title = title;
            OkLabel = okLabel;
            CancelLabel = cancelLabel;
            Items = items;
            CheckedItems = checkedItems;
            OnComplete = onComplete;
        }

        public string CancelLabel { get; private set; }
        public bool[] CheckedItems { get; private set; }
        public IEnumerable Items { get; private set; }
        public string OkLabel { get; private set; }
        public Action<List<int>> OnComplete { get; private set; }
        public string Title { get; private set; }
    }
}