using artm.Fetcher.Core.Services;
using artm.MvxPlugins.Dialog.Models;
using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Dialog.ViewModels;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        private readonly IDialogService _dialog;

        private readonly DialogServiceMultiItem[] _allItems;
        private List<bool> _checkedItems = new List<bool>();

        public FirstViewModel(IFetcherService fetcher, IDialogService dialog)
        {
            _dialog = dialog;

                var data = fetcher.Fetch(new Uri("https://services.coop.dk/restgrundsortiment/api/Vare/24444")).Result;
                System.Diagnostics.Debug.WriteLine(data.Response);
                var debug = 42;

            var items = new List<DialogServiceMultiItem>();
            items.Add(new DialogServiceMultiItem("a", "blabla"));
            items.Add(new DialogServiceMultiItem("b", "blabla"));
            items.Add(new DialogServiceMultiItem("c", "blabla"));
            items.Add(new DialogServiceMultiItem("d", "blabla"));
            items.Add(new DialogServiceMultiItem("e", "blabla"));
            items.Add(new DialogServiceMultiItem("f", "blabla"));
            items.Add(new DialogServiceMultiItem("g", "blabla"));
            items.Add(new DialogServiceMultiItem("h", "blabla"));
            items.Add(new DialogServiceMultiItem("i", "blabla"));
            items.Add(new DialogServiceMultiItem("j", "blabla"));
            items.Add(new DialogServiceMultiItem("k", "blabla"));
            items.Add(new DialogServiceMultiItem("l", "blabla"));
            _allItems = items.ToArray();

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
    }
}
