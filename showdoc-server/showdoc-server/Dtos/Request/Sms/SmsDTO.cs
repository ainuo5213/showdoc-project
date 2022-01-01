using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Attributes;

namespace showdoc_server.Dtos.Request.Sms
{
    public class SmsDTO
    {
        [Required(ErrorMessage = "cellphone needed")]
        [Cellphone(ErrorMessage = "not a phone number")]
        public string Cellphone { get; set; }

        [Required(ErrorMessage = "type needed")]
        public SmsTypes Type { get; set; }
    }
}
