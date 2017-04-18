﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using artm.MvxPlugins.Fetcher.Entities;
using Realms;

namespace artm.MvxPlugins.Fetcher.Services
{
    public class FetcherRepositoryService : IFetcherRepositoryService
    {
        private readonly Realm _realm;

        public FetcherRepositoryService()
        {
            _realm = Realm.GetInstance();
        }

        public IUrlCacheInfo GetEntryForUrl(Uri url)
        {
            UrlCacheInfo data;

            var needle = url.OriginalString;
            try
            {
                data = _realm.All<UrlCacheInfo>().Where(x => x.Url == needle).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }

            return data;
        }

        public void UpdateLastAccessed(IUrlCacheInfo hero)
        {
            _realm.Write(() => hero.LastAccessed = DateTimeOffset.UtcNow);
        }

        public void UpdateUrl(Uri uri, IUrlCacheInfo hero, string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                return;
            }

            var timestamp = DateTime.UtcNow;
            _realm.Write(() =>
            {
                hero.Response = response;
                hero.Url = uri.OriginalString;
                hero.LastAccessed = timestamp;
                hero.LastUpdated = timestamp;
            });
        }

        public IUrlCacheInfo InsertUrl(Uri uri, string response)
        {
            UrlCacheInfo hero = null;
            if (string.IsNullOrEmpty(response))
            {
                return hero;
            }

            var existing = GetEntryForUrl(uri) as UrlCacheInfo;
            if(existing != null)
            {
                _realm.Remove(existing);
            }

            var timestamp = DateTime.UtcNow;
            _realm.Write(() =>
            {
                hero = new UrlCacheInfo();
                hero.Response = response;
                hero.Url = uri.OriginalString;
                hero.Created = timestamp;
                hero.LastAccessed = timestamp;
                hero.LastUpdated = timestamp;
                _realm.Add(hero);
            });

            return hero;
        }
    }
}