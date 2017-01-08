using artm.MvxPlugins.Dialog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Dialog.Touch.Services
{
    public class DialogService : IDialogService
    {
        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void LoadingProgress(int progress)
        {
            throw new NotImplementedException();
        }

        public void ShowLoading(string message, bool withProgress = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> ShowMultipleChoice(string[] items, bool[] checkedItems, string positiveLabel = "Okay")
        {
            throw new NotImplementedException();
        }
    }
}