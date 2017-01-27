using artm.MvxPlugins.Dialog.Droid.Services;
using artm.MvxPlugins.Dialog.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
        private readonly string[] _allItems = new string[] { "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "a", "b", "c" };
        private List<bool> _checkedItems = new List<bool>();
        public FirstViewModel(IDialogService dialog)
        {
            _dialog = dialog;

            InitializeCheckedItems();
        }

        private void InitializeCheckedItems()
        {
            var random = new Random();
            foreach (var item in _allItems)
            {
                var rand = random.Next(0, 2);
                if (rand == 1)
                {
                    _checkedItems.Add(true);
                }
                else
                {
                    _checkedItems.Add(false);
                }
            }
        }

        private MvxCommand _showSecondViewModelCommand;
        public MvxCommand ShowSecondViewModelCommand
        {
            get
            {
                _showSecondViewModelCommand = _showSecondViewModelCommand ?? new MvxCommand(DoShowSecondViewModelCommand);
                return _showSecondViewModelCommand;
            }
        }

        private void DoShowSecondViewModelCommand()
        {
            ShowViewModel<SecondViewModel>();
        }


        private MvxAsyncCommand _showListCommand;
        public MvxAsyncCommand ShowListCommand
        {
            get
            {
                _showListCommand = _showListCommand ?? new MvxAsyncCommand(() => DoShowListCommandAsync("MyTitle"));
                return _showListCommand;
            }
        }

        private MvxAsyncCommand _showListWithTitleCommand;
        public MvxAsyncCommand ShowListWithTitleCommand
        {
            get
            {
                _showListWithTitleCommand = _showListWithTitleCommand ?? new MvxAsyncCommand(() => DoShowListCommandAsync("Alt title"));
                return _showListWithTitleCommand;
            }
        }

        private async Task DoShowListCommandAsync(string title)
        {
            try
            {
                var bundle = new DialogServiceMultiItemsBundle(title, _allItems, _checkedItems.ToArray());
                var result = await _dialog.ShowMultipleChoice(bundle);
                for (int i = 0; i < _checkedItems.Count; i++)
                {
                    _checkedItems[i] = false;
                }

                foreach (var index in result)
                {
                    _checkedItems[index] = true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
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
