using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.User
{
    public class SearchUserJoinProjectItemDTO
    {
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string HeadImg { get; internal set; }
    }
}
