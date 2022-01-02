using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Request.Invitation
{
    public enum InvitationCheckStatuses
    {
        Rejected = -1,
        Checking = 0,
        Passed = 1
    }
}
