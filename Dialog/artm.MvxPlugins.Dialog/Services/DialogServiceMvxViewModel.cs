using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MvvmCross.Core.ViewModels;
using artm.MvxPlugins.Dialog.Services;

namespace artm.MvxPlugins.Dialog.Services
{
    public abstract class DialogServiceMvxViewModel : MvxViewModel
    {
        public void ShowAndroidDetails()
        {
            ShowViewModel<MultiChoiceDetailsViewModel>();
        }
    }
}