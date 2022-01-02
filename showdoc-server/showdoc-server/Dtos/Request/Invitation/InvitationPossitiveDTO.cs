using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Invitation
{
    /// <summary>
    /// 主动邀请
    /// </summary>
    public class InvitationPossitiveDTO
    {
        public int Invited { get; set; }
        public int ProjectID { get; set; }
    }
}
