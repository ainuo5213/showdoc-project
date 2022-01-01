using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class Users
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string HeadImg { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
