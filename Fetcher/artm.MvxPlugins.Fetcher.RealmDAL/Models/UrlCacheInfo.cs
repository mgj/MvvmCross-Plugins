using Realms;
using System;

namespace artm.MvxPlugins.Fetcher.Entities
{
    public class UrlCacheInfo : RealmObject, IUrlCacheInfo
    {
        public string Response { get; set; }

        [Indexed]
        public string Url { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset LastAccessed { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
    }
}
