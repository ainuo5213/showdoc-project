using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Services.Sms
{
    public interface ISmsService
    {
        Task<bool> SendSmsCode(string prefix, string cellphone);
    }
}
