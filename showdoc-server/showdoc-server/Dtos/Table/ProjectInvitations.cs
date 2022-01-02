using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Request.Invitation;
using SqlSugar;

namespace showdoc_server.Dtos.Table
{
    public class ProjectInvitations
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ProjectInvitationID { get; set; }
        public int UserID { get; set; }
        public int InviteByID { get; set; }
        public int ProjectID { get; set; }
        public InvitationCheckStatuses Status { get; set; } = InvitationCheckStatuses.Checking;
        public int CheckUserID { get; set; }
        public DateTime CheckTime { get; set; } = new DateTime(1900, 1, 1);
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
