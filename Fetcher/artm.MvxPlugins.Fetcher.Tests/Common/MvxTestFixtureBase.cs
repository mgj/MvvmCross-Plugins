﻿using MvvmCross.Core.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Test.Core;

namespace artm.MvxPlugins.Fetcher.Tests.Common
{
    public class MvxTestFixtureBase : MvxIoCSupportingTest
    {
        private MockDispatcher _mockDispatcher;

        public MvxTestFixtureBase()
        {
            Setup();
        }

        protected override void AdditionalSetup()
        {
            // Setup MVVMCross for testing
            _mockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(_mockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(_mockDispatcher);
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

            // Register our own services

        }
    }
}
