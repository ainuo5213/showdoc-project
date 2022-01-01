using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using showdoc_server.Dtos.Json;

namespace showdoc_server.Utils.Json
{
    public class AppSetting
    {
        private static readonly IConfigurationRoot _configuration;
        static AppSetting()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            _configuration = builder.Build();
        }
        public static ConnectionInstance GetConnectionConfig()
        {
            var userId = _configuration["Connection:UserId"];
            var password = _configuration["Connection:Password"];
            var ip = _configuration["Connection:IP"];
            var database = _configuration["Connection:Database"];
            return new ConnectionInstance()
            {
                 Database = database,
                 UserId = userId,
                 Password = password,
                 IP = ip,
            };
        }
    }
}
