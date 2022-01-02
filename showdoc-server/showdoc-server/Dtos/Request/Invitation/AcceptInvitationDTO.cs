using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Invitation
{
    public class AcceptInvitationDTO
    {
        public int InvitationID { get; set; }
        public InvitationCheckStatuses Status { get; set; }
    }
}
