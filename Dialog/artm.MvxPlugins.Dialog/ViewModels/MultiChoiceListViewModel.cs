using artm.MvxPlugins.Dialog.Services;
using MvvmCross.Core.ViewModels;

namespace artm.MvxPlugins.Dialog.ViewModels
{
    public class MultiChoiceListViewModel : MvxViewModel
    {
        private readonly IDialogService _dialog;

        public MultiChoiceListViewModel(IDialogService dialog)
        {
            _dialog = dialog;
        }
    }
}
