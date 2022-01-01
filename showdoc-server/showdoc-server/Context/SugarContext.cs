using System;
using showdoc_server.Utils.Json;
using SqlSugar;

namespace showdoc_server.Context
{
    public class SugarContext
    {
        public static SqlSugarClient Context
        {
            get => new SqlSugarClient(new ConnectionConfig()
            {
                ConfigId = AppSetting.GetConnectionConfig().IP,
                ConnectionString = AppSetting.GetConnectionConfig().ToString(),
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                AopEvents = new AopEvents()
                {
                    OnLogExecuting = (sql, parameters) =>
                    {
                        Console.WriteLine(sql);
                    }
                },
                InitKeyType = InitKeyType.SystemTable
            });
        }
    }
}
