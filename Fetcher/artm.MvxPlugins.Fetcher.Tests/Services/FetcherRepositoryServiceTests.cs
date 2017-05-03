using artm.MvxPlugins.Fetcher.Services;
using artm.MvxPlugins.Fetcher.Tests.Common;
using artm.MvxPlugins.Fetcher.Tests.Services.Calculator;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Tests.Services
{
    [TestFixture]
    public class FetcherRepositoryServiceTests
    {
        [Test]
        public void GetEntryForUrl_NoEntryExists_NullIsReturned()
        {
            var url = new Uri("https://www.google.com");
            var sut = FetcherRepositoryServiceFactory();

            var entry = sut.GetEntryForUrl(url);

            Assert.IsNull(entry);
        }

        [Test]
        public void GetEntryForUrl_EntryExists_EntryReturned()
        {
            var url = new Uri("https://www.google.com");
            var sut = FetcherRepositoryServiceFactory();

            sut.InsertUrl(url, "myResponse");
            var entry = sut.GetEntryForUrl(url);

            Assert.IsNotNull(entry);
        }

        private static FetcherRepositoryService FetcherRepositoryServiceFactory()
        {
            return new FetcherRepositoryService(FetcherRepositoryStoragePathServiceFactory());
        }

        private static IFetcherRepositoryStoragePathService FetcherRepositoryStoragePathServiceFactory()
        {
            var result = new Mock<IFetcherRepositoryStoragePathService>();

            result.Setup(x => x.GetPath(It.IsAny<string>())).Returns(() => ":memory:");

            return result.Object;
        }
    }
}
