using artm.MvxPlugins.Dialog.Models;
using System.Collections.Generic;
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

        Task<List<int>> ShowMultipleChoice(DialogServiceMultiItemsBundle bundle);
        DialogServiceMultiItemsBundle LastBundle { get; }
    }
}
