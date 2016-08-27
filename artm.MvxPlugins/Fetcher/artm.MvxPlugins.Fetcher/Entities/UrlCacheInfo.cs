using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
