
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Invitation
{
    public class InvitationListItemDTO
    {
        public int InvitationID { get; set; }
        public int Invited { get; set; }
        public string InvitedUsername { get; set; }
        public InvitationCheckStatuses Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CheckTime { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
}
