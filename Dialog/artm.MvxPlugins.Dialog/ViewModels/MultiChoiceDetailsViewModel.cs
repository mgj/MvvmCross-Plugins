using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MvvmCross.Core.ViewModels;
using artm.MvxPlugins.Dialog.Services;

namespace artm.MvxPlugins.Dialog.ViewModels
{
    public class MultiChoiceDetailsViewModel : MvxViewModel
    {
        private readonly IDialogService _dialog;

        public MultiChoiceDetailsViewModel(IDialogService dialog)
        {
            _dialog = dialog;
        }
    }
}