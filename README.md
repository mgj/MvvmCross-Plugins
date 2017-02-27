MvxPlugins
==========

[![Build status](https://ci.appveyor.com/api/projects/status/8iekgrnckblfgfb6?svg=true)](https://ci.appveyor.com/project/mgj/mvvmcross-plugins)

| Plugin          | NuGet version                                                                                                                                                              |
| --------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Fetcher        | [![NuGet](https://img.shields.io/nuget/v/artm.MvxPlugins.Fetcher.svg)](https://www.nuget.org/packages/artm.MvxPlugins.Fetcher/)             |
| Logger         | [![NuGet](https://img.shields.io/nuget/v/artm.MvxPlugins.Logger.svg)](https://www.nuget.org/packages/artm.MvxPlugins.Logger/)               |
| Dialog         | [![NuGet](https://img.shields.io/nuget/v/artm.MvxPlugins.Dialog.svg)](https://www.nuget.org/packages/artm.MvxPlugins.Dialog/)               |

Dialog
=======
1. Add to setup.cs:

        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewAssemblies());
            list.Add(typeof(MultiChoiceListView).Assembly);
            return list.ToArray();
        }

        protected override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewModelAssemblies());
            list.Add(typeof(MultiChoiceListViewModel).Assembly);
            return list.ToArray();
        }

2. Inherit from:
DialogServiceMvxViewModelBase

-----
    
        DialogService::Task<List<int>> ShowMultipleChoicePopupAndroid(DialogServiceMultiItemsBundle bundle) 
        
is available for a popup dialog multichoice that does *NOT* support descriptions

License
=======

- **Fetcher** plugin is licensed under [Apache 2.0][apache]
- **Logger** plugin is licensed under [Apache 2.0][apache]
- **Dialog** plugin is licensed under [Apache 2.0][apache]

[apache]: https://www.apache.org/licenses/LICENSE-2.0.html