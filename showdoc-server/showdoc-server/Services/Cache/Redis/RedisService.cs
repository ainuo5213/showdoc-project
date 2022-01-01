using System;
using StackExchange.Redis;

namespace showdoc_server.Services.Cache.Redis
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer Redis;
        private readonly IDatabase DB;

        public RedisService()
        {
            this.Redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            this.DB = this.Redis.GetDatabase();
        }

        public void Delete(string key)
        {
            this.DB.KeyDelete(key);
        }

        public string Get(string key)
        {
            return this.DB.StringGet(key);
        }

        public void Set(string key, string value)
        {
            this.DB.StringSet(key, value);
        }

        public void Set(string key, string value, TimeSpan expires)
        {
            this.DB.StringSet(key, value, expires);
        }

        public string Key(string prefix, string cellphone)
        {
            return $"{prefix}:{cellphone}";
        }
    }
}
