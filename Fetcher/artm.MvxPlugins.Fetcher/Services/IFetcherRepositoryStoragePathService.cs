﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artm.MvxPlugins.Fetcher.Services
{
    public interface IFetcherRepositoryStoragePathService
    {
        string GetPath(string filename = "fetcher.db3");
    }
}
