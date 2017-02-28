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

namespace artm.MvxPlugins.Dialog.Droid.Services
{
    public class DialogServiceException : Exception
    {
        public DialogServiceException(string msg) : base(msg)
        {
        }
    }
}