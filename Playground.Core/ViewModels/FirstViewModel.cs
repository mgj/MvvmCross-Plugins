using artm.MvxPlugins.Dialog.Services;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
        public FirstViewModel(IDialogService dialog)
        {
            _dialog = dialog;
        }

        private MvxAsyncCommand _showListCommand;
        public MvxAsyncCommand ShowListCommand
        {
            get
            {
                _showListCommand = _showListCommand ?? new MvxAsyncCommand(DoShowListCommandAsync);
                return _showListCommand;
            }
        }

        private async Task DoShowListCommandAsync()
        {
            var items = new string[]{ "a", "b", "c" };
            var checkedItems = new bool[] { true, false, false };
            var result = await _dialog.ShowMultipleChoice(items, checkedItems);
            var debug = 42;
        }


        private string _hello = "Hello MvvmCross";
        private readonly IDialogService _dialog;

        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }
    }
}
