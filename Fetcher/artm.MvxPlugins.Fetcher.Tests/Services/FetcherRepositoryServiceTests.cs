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
        [Ignore("Realm needs windows support to run tests on windows")]
        public void GetEntryForUrl_NoEntryExists_NullIsReturned()
        {
            //var url = new Uri("https://www.google.com");
            //var sut = new FetcherRepositoryService();

            //var entry = sut.GetEntryForUrl(url);

            //Assert.IsNull(entry);
        }

        [Test]
        [Ignore("Realm needs windows support to run tests on windows")]
        public void GetEntryForUrl_EntryExists_EntryReturned()
        {
            //var url = new Uri("https://www.google.com");
            //var sut = new FetcherRepositoryService();

            //sut.InsertUrl(url, "myResponse");
            //var entry = sut.GetEntryForUrl(url);

            //Assert.IsNotNull(entry);
        }
    }
}
