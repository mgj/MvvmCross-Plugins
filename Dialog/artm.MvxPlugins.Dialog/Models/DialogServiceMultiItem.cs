namespace artm.MvxPlugins.Dialog.Models
{
    public class DialogServiceMultiItem
    {
        public DialogServiceMultiItem(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Description { get; internal set; }
        public string Title { get; private set; }
    }
}