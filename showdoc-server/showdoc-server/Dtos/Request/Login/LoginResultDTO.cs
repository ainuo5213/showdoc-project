using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Login
{
    public class LoginResultDTO
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string HeadImg { get; set; }
        public string Token { get; set; }
    }
}
