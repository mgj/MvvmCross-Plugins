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
        private AlertDialog _alertDialog;
        private AlertDialog.Builder _builder;

        public void Info(string message)
        {
            if (CurrentContext == null)
            {
                return;
            }

            CurrentContext.RunOnUiThread(() =>
            {
                if (_alertDialog != null && _alertDialog.IsShowing)
                {
                    _alertDialog.SetMessage(message);
                }
                else
                {
                    _alertDialog = new AlertDialog.Builder(CurrentContext)
                        .SetPositiveButton("OK", (sender, e) => { })
                        .SetMessage(message)
                        .Show();
                }
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
            
            if (IsNewContext())
            {
                _builder = new AlertDialog.Builder(CurrentContext);
            }

            ConfigureBuilder(bundle, tcs);
            _builder.Show();

            return await tcs.Task;
        }

        private bool IsNewContext()
        {
            if (_builder == null) return true;

            var package = (ContextWrapper)_builder.Context as ContextWrapper;
            var bContext = package.BaseContext as Activity;
            var cConext = CurrentContext as Activity;
            if(bContext.LocalClassName.Equals(cConext.LocalClassName))
            {
                return false;
            }
            return true;
        }

        private void ConfigureBuilder(DialogServiceMultiItemsBundle bundle, TaskCompletionSource<List<int>> tcs)
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
            var orgCheckedItemsIndex = new List<int>(checkedItemsIndex);

            _builder.SetMultiChoiceItems(bundle.Items, bundle.CheckedItems, (sender, e) =>
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

            _builder.SetPositiveButton(bundle.PositiveLabel, (sender, e) =>
            {
                tcs.SetResult(checkedItemsIndex);
            });

            _builder.SetNegativeButton(bundle.NegativeLabel, (sender, e) =>
            {
                tcs.SetResult(orgCheckedItemsIndex);
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