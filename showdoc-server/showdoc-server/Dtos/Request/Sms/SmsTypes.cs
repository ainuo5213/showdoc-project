using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Sms
{
    public enum SmsTypes
    {
        [Description("register")]
        Register = 0,
        [Description("forgetPassword")]
        ForgetPassword = 1 
    }
}
