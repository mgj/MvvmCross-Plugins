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
            var result = await _dialog.ShowMultipleChoice(_allItems, _checkedItems.ToArray());
            for (int i = 0; i < _checkedItems.Count; i++)
            {
                _checkedItems[i] = false;
            }

            foreach (var index in result)
            {
                _checkedItems[index] = true;
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
