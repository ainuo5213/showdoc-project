using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.User
{
    public class UserInfoDTO
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Username { get; set; }
        public string HeadImg { get; set; }
    }
}
