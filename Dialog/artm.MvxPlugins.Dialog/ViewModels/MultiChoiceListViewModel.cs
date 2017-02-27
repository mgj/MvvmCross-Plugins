using artm.MvxPlugins.Dialog.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
