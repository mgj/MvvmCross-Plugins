namespace artm.MvxPlugins.Dialog.Models
{
    public class DialogServiceMultiItemsBundle
    {
        public DialogServiceMultiItemsBundle(string title, DialogServiceMultiItem[] items, bool[] checkedItems, string positiveLabel = "Okay", string negativeLabel = "Cancel")
        {
            Title = title;
            Items = items;
            CheckedItems = checkedItems;
            PositiveLabel = positiveLabel;
            NegativeLabel = negativeLabel;
        }

        public bool[] CheckedItems { get; private set; }
        public DialogServiceMultiItem[] Items { get; private set; }
        public string NegativeLabel { get; private set; }
        public string PositiveLabel { get; private set; }
        public string Title { get; private set; }
    }
}