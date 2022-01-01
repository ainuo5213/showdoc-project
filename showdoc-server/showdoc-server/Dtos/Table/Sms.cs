using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class Sms
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int SmsID { get; set; }
        public string Cellphone { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool Status { get; set; }
    }
}
