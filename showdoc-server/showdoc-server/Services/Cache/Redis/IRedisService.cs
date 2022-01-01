using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Services.Cache.Redis
{
    public interface IRedisService
    {
        void Set(string key, string value);
        string Get(string key);
        void Delete(string key);
        void Set(string key, string value, TimeSpan expires);
        string Key(string prefix, string cellphone);
    }
}
