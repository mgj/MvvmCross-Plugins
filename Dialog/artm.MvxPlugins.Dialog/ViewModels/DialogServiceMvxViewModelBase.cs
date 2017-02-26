using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MvvmCross.Core.ViewModels;
using artm.MvxPlugins.Dialog.Services;

namespace artm.MvxPlugins.Dialog.ViewModels
{
    public abstract class DialogServiceMvxViewModelBase : MvxViewModel
    {
        public void ShowAndroidDetails()
        {
            ShowViewModel<MultiChoiceListViewModel>();
        }
    }
}