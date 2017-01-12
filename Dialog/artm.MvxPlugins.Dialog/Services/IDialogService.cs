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

        /// <summary>
        /// Update the progress for a "Loading" dialog that has already been shown.
        /// If 100 is passed as the progress argument, the dialog is dismissed.
        /// </summary>
        /// <param name="progress">Integer from 0-100 indicating progress in percent</param>
        void LoadingProgress(int progress);

        Task<List<int>> ShowMultipleChoice(string title, string[] items, bool[] checkedItems, string positiveLabel = "Okay");
    }
}
