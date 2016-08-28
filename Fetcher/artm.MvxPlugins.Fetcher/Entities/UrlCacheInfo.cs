using Realms;

namespace artm.MvxPlugins.Fetcher.Entities
{
    public class UrlCacheInfo : RealmObject
    {
        public string Response { get; set; }

        [Indexed]
        public string Url { get; set; }

        public long Created { get; set; }

        public long LastAccessed { get; set; }

        public long LastUpdated { get; set; }
    }
}
