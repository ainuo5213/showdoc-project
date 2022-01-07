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

        public async Task<int> Count(string cellphone)
        {
            var begin = DateTime.Now.Date;
            var end = DateTime.Now.Date.AddDays(1);
            return await SugarContext.Context.Queryable<Dtos.Table.Sms>().CountAsync(sms => sms.Cellphone == cellphone && sms.CreateTime >= begin && sms.CreateTime < end);
        }
    }
}
