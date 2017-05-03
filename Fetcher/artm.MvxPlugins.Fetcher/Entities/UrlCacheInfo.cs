using artm.MvxPlugins.Fetcher.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Entities
{
    public class UrlCacheInfo : IUrlCacheInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Response { get; set; }

        [Indexed(Name = "Url", Unique = true)]
        public string Url { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset LastAccessed { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
    }
}
