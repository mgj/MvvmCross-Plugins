using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using artm.MvxPlugins.Dialog.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Dialog.Droid.Services
{
    public class DialogService : IDialogService
    {
        private ProgressDialog _progressDialog;
        private DialogServiceMultiItemsBundle _lastMultipleItemsBundle;
        private AlertDialog _lastMultipleChoiceDialog;

        public void Info(string message)
        {
            if (CurrentContext == null)
            {
                return;
            }

            CurrentContext.RunOnUiThread(() =>
            {
                new AlertDialog.Builder(CurrentContext)
                        .SetPositiveButton("OK", (sender, e) => { })
                        .SetMessage(message)
                        .Show();
            });
        }

        public void ShowLoading(string message, bool withProgress = false)
        {
            if (CurrentContext == null)
            {
                return;
            }

            CurrentContext.RunOnUiThread(() =>
            {
                if (_progressDialog == null || (_progressDialog != null && (_progressDialog.IsShowing == false)))
                {
                    _progressDialog = ProgressDialogFactory(CurrentContext, message, withProgress);
                }

                if (_progressDialog.IsShowing == false)
                {
                    _progressDialog.Show();
                }
            });
        }

        public void LoadingProgress(int progress)
        {
            if (_progressDialog == null || CurrentContext == null)
            {
                return;
            }

            CurrentContext.RunOnUiThread(() =>
            {
                _progressDialog.Progress = progress;
                if (progress == 100)
                {
                    _progressDialog.Dismiss();
                }
            });

        }

        public async Task<List<int>> ShowMultipleChoice(DialogServiceMultiItemsBundle bundle)
        {
            var tcs = new TaskCompletionSource<List<int>>();
            
            var builder = new AlertDialog.Builder(CurrentContext);
            
            // Attempt to re-use last dialog to increate performance
            // If only the title is different, we can still re-use it
            if (DialogServiceMultiItemsBundle.SameValuesAs(_lastMultipleItemsBundle, bundle) == false 
                && DialogServiceMultiItemsBundle.SameItemsAs(_lastMultipleItemsBundle, bundle) 
                && DialogServiceMultiItemsBundle.SameCheckedItemsAs(_lastMultipleItemsBundle, bundle))
            {
                _lastMultipleChoiceDialog.SetTitle(bundle.Title);
                UpdateTaskCompletionSource(_lastMultipleChoiceDialog, _lastMultipleItemsBundle, tcs);
            }
            else
            {
                ConfigureBuilder(builder, bundle);
                UpdateTaskCompletionSource(builder, bundle, tcs);
                _lastMultipleChoiceDialog = builder.Create();
            }

            _lastMultipleItemsBundle = bundle;
            _lastMultipleChoiceDialog.Show();

            return await tcs.Task;
        }

        private static void ConfigureBuilder(AlertDialog.Builder builder, DialogServiceMultiItemsBundle bundle)
        {
            var checkedItemsIndex = GetIndexOfCheckedItems(bundle);
            var orgCheckedItemsIndex = new List<int>(checkedItemsIndex);

            builder.SetTitle(bundle.Title);
            builder.SetMultiChoiceItems(bundle.Items, bundle.CheckedItems, (sender, e) =>
            {
                if (e.IsChecked)
                {
                    checkedItemsIndex.Add(e.Which);
                }
                else if (checkedItemsIndex.Contains(e.Which))
                {
                    checkedItemsIndex.Remove(e.Which);
                }
            });
        }

        private static void UpdateTaskCompletionSource(AlertDialog dialog, DialogServiceMultiItemsBundle bundle, TaskCompletionSource<List<int>> tcs)
        {
            var checkedItemsIndex = GetIndexOfCheckedItems(bundle);
            var orgCheckedItemsIndex = new List<int>(checkedItemsIndex);

            dialog.SetButton((int)DialogButtonType.Positive, bundle.PositiveLabel, (sender, e) =>
            {
                tcs.SetResult(checkedItemsIndex);
            });

            dialog.SetButton((int)DialogButtonType.Negative, bundle.PositiveLabel, (sender, e) =>
            {
                tcs.SetResult(orgCheckedItemsIndex);
            });
        }

        private static void UpdateTaskCompletionSource(AlertDialog.Builder builder, DialogServiceMultiItemsBundle bundle, TaskCompletionSource<List<int>> tcs)
        {
            var checkedItemsIndex = GetIndexOfCheckedItems(bundle);
            var orgCheckedItemsIndex = new List<int>(checkedItemsIndex);

            builder.SetPositiveButton(bundle.PositiveLabel, (sender, e) =>
            {
                tcs.SetResult(checkedItemsIndex);
            });

            builder.SetNegativeButton(bundle.NegativeLabel, (sender, e) =>
            {
                tcs.SetResult(orgCheckedItemsIndex);
            });
        }
        

        private static List<int> GetIndexOfCheckedItems(DialogServiceMultiItemsBundle bundle)
        {
            var checkedItemsIndex = new List<int>();
            for (int i = 0; i < bundle.CheckedItems.Length; i++)
            {
                var hero = bundle.CheckedItems[i];
                if (hero == true)
                {
                    checkedItemsIndex.Add(i);
                }
            }

            return checkedItemsIndex;
        }

        private bool IsNewContext(AlertDialog.Builder builder)
        {
            if (builder == null) return true;

            var package = (ContextWrapper)builder.Context as ContextWrapper;
            var bContext = package.BaseContext as Activity;
            var cConext = CurrentContext as Activity;
            if(bContext.LocalClassName.Equals(cConext.LocalClassName))
            {
                return false;
            }
            return true;
        }

        private Activity CurrentContext
        {
            get
            {
                if (Mvx.CanResolve<IMvxAndroidCurrentTopActivity>())
                {
                    return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
                }

                return null;
            }
        }
    }
}