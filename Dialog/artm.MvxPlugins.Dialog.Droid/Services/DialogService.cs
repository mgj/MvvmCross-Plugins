using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using artm.MvxPlugins.Dialog.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Threading.Tasks;
using artm.MvxPlugins.Logger.Services;
using MvvmCross.Droid.Views;
using artm.MvxPlugins.Dialog.ViewModels;
using artm.MvxPlugins.Dialog.Models;

namespace artm.MvxPlugins.Dialog.Droid.Services
{
    public class DialogService : IDialogService
    {
        private ProgressDialog _progressDialog;
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

        private static ProgressDialog ProgressDialogFactory(Context context, string message, bool withProgress)
        {
            var dialog = new ProgressDialog(context);
            if (withProgress)
            {
                dialog.SetProgressStyle(ProgressDialogStyle.Horizontal);
            }
            else
            {
                dialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            }

            dialog.SetMessage(message);

            return dialog;
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

            // Attempt to re-use last dialog to increate performance
            // If only the title is different, we can still re-use it
            if (LastBundle.SameValuesAs(bundle) == false 
                && LastBundle.SameItemsAs(bundle) 
                && LastBundle.SameCheckedItemsAs(bundle))
            {
                _lastMultipleChoiceDialog.SetTitle(bundle.Title);
                UpdateTaskCompletionSource(_lastMultipleChoiceDialog, LastBundle, tcs);
            }
            else
            {
                
                var builder = new AlertDialog.Builder(CurrentContext);
                ConfigureBuilder(builder, bundle, tcs);
                _lastMultipleChoiceDialog = builder.Create();
            }

            LastBundle = bundle;

            try
            {
                _lastMultipleChoiceDialog.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                throw ex;
            }

            return await tcs.Task;
        }

        private void ConfigureBuilder(AlertDialog.Builder builder, DialogServiceMultiItemsBundle bundle, TaskCompletionSource<List<int>> tcs)
        {
            var checkedItemsIndex = GetIndexOfCheckedItems(bundle);
            var orgCheckedItemsIndex = new List<int>(checkedItemsIndex);

            builder.SetTitle(bundle.Title);

            var titles = bundle.Items.Select(x => x.Title).ToArray();
            builder.SetMultiChoiceItems(titles, bundle.CheckedItems, (sender, e) =>
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

            builder.SetPositiveButton(bundle.PositiveLabel, (sender, e) =>
            {
                if(tcs.TrySetResult(checkedItemsIndex) == false)
                {
                    Console.WriteLine("Unable to set CHECKED_ITEMS result in builder");

                }
            });

            builder.SetNegativeButton(bundle.NegativeLabel, (sender, e) =>
            {
                if (tcs.TrySetResult(orgCheckedItemsIndex) == false)
                {
                    Console.WriteLine("Unable to set ORG_CHECKED_ITEMS result in builder");
                }
            });
            builder.SetOnDismissListener(new MyDismissListener(tcs, orgCheckedItemsIndex));
        }

        private void UpdateTaskCompletionSource(AlertDialog dialog, DialogServiceMultiItemsBundle bundle, TaskCompletionSource<List<int>> tcs)
        {
            var checkedItemsIndex = GetIndexOfCheckedItems(bundle);
            var orgCheckedItemsIndex = new List<int>(checkedItemsIndex);

            dialog.SetButton((int)DialogButtonType.Positive, bundle.PositiveLabel, (sender, e) =>
            {
                if (tcs.TrySetResult(checkedItemsIndex) == false)
                {
                    Console.WriteLine("Unable to set CHECKED_ITEMS result in update tcs");
                }
            });

            dialog.SetButton((int)DialogButtonType.Negative, bundle.PositiveLabel, (sender, e) =>
            {
                if (tcs.TrySetResult(orgCheckedItemsIndex) == false)
                {
                    Console.WriteLine("Unable to set ORG_CHECKED_ITEMS result in update tcs");
                }
            });
            dialog.SetOnDismissListener(new MyDismissListener(tcs, orgCheckedItemsIndex));
        }

        private class MyDismissListener : Java.Lang.Object, IDialogInterfaceOnDismissListener
        {
            private readonly List<int> _checkedItemsIndex;
            private readonly ILoggerService _log;
            private readonly TaskCompletionSource<List<int>> _tcs;

            public MyDismissListener(TaskCompletionSource<List<int>> tcs, List<int> checkedItemsIndex)
            {
                _log = Mvx.Resolve<ILoggerService>();
                _tcs = tcs;
                _checkedItemsIndex = checkedItemsIndex;
            }

            public void OnDismiss(IDialogInterface dialog)
            {
                if (_tcs == null) return;
                if(_tcs.TrySetResult(_checkedItemsIndex) == false)
                {
                    Console.WriteLine("Unable to set result in dismisslistener");
                }
            }
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

        public DialogServiceMultiItemsBundle LastBundle
        {
            get;
            private set;
        }
        public TaskCompletionSource<List<int>> LastTcs { get; private set; }
    }
}