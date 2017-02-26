using artm.MvxPlugins.Dialog.Services;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using artm.MvxPlugins.Dialog.Droid.Services;

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

        public Task<List<int>> ShowMultipleChoice(DialogServiceMultiItemsBundle bundle)
        {
            var tcs = new TaskCompletionSource<List<int>>();
            LastBundle = bundle;

            var navigationController = (Mvx.Resolve<IMvxIosViewPresenter>() as MvxIosViewPresenter).MasterNavigationController;

            var multiChoiceController = new MultiChoiceViewController(LastBundle, (selectedItems) =>
            {
                tcs.SetResult(selectedItems);
            });


            multiChoiceController.View.BackgroundColor = UIColor.White;
            multiChoiceController.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;
            multiChoiceController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;

            navigationController.VisibleViewController.PresentViewController(multiChoiceController, true, null);

            return tcs.Task;
        }

        public DialogServiceMultiItemsBundle LastBundle { get; private set; }
    }
}