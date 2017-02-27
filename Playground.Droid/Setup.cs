using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using artm.MvxPlugins.Dialog.Services;
using artm.MvxPlugins.Dialog.Droid.Views;
using artm.MvxPlugins.Logger.Services;
using artm.MvxPlugins.Logger.Droid.Services;
using System.Collections.Generic;
using System.Reflection;
using artm.MvxPlugins.Dialog.ViewModels;
using artm.MvxPlugins.Dialog.Droid.Services;
using Android.App;
using MvvmCross.Platform.Droid.Platform;

namespace Playground.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

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

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            var viewLifecycleManager = new DroidViewLifecycleManager();
            var app = ApplicationContext as Application;
            app.RegisterActivityLifecycleCallbacks(viewLifecycleManager);
            Mvx.RegisterSingleton<IMvxAndroidCurrentTopActivity>(viewLifecycleManager);
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            Mvx.LazyConstructAndRegisterSingleton<ILoggerService>(() => new LoggerService(ApplicationContext));
            Mvx.ConstructAndRegisterSingleton<IDialogService, DialogService>();
        }
    }
}
