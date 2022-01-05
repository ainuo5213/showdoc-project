using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Reponsitory.Sms
{
    public interface ISmsReponsitory
    {
        Task<int> AddSms(Dtos.Table.Sms entity);
        Task<int> Count(string cellphone);
    }
}
