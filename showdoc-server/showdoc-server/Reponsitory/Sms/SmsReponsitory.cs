using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Context;

namespace showdoc_server.Reponsitory.Sms
{
    public class SmsReponsitory : ISmsReponsitory
    {
        public async Task<int> AddSms(Dtos.Table.Sms entity)
        {
            return await SugarContext.Context.Insertable(entity).ExecuteCommandAsync();
        }
    }
}
