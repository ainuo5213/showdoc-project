using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Invitation;

namespace showdoc_server.Reponsitory.Invitation
{
    public interface IInvitationReponsitory
    {
        Task<int> InviteJoinProject(int userID, int invited, int projectID);
        Task<int> JoinProject(int applyID, int projectID);
        Task<ListItemDTO<InvitationListItemDTO>> InvitationList(int userID, int page);
        Task<int> AcceptInvitation(int userID, int invitationID, InvitationCheckStatuses acceptStatus);
    }
}
