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

        public async Task<List<int>> ShowMultipleChoice(string title, string[] items, bool[] checkedItems, string positiveLabel = "Okay")
        {
            var tcs = new TaskCompletionSource<List<int>>();
            var result = new List<int>();

            // We want the checkedItems to still be in the list, even when they have not been actively clicked
            for (int i = 0; i < checkedItems.Length; i++)
            {
                var hero = checkedItems[i];
                if(hero == true)
                {
                    result.Add(i);
                }
            }

            var builder = new AlertDialog.Builder(CurrentContext);
            if(string.IsNullOrEmpty(title) == false)
            {
                builder.SetTitle(title);
            }

            builder.SetMultiChoiceItems(items, checkedItems, (sender, e) =>
            {
                if (e.IsChecked)
                {
                    result.Add(e.Which);
                }
                else if (result.Contains(e.Which))
                {
                    result.Remove(e.Which);
                }
            });
            
            builder.SetPositiveButton(positiveLabel, (sender, e) =>
            {   
                tcs.SetResult(result);
            });

            builder.Show();

            return await tcs.Task;
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