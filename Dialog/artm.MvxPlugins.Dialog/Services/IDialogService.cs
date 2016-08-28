using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Dialog.Services
{
    public interface IDialogService
    {
        void Info(string message);
        void ShowLoading(string message, bool withProgress = false);
        void LoadingProgress(int progress);
    }
}
