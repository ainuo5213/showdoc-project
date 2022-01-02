using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Invitation;

namespace showdoc_server.Services.Invitation
{
    public interface IInvitationService
    {
        Task<bool> InviteJoinProject(int invite, int invited, int projectID);
        Task<bool> JoinProject(int applyID, int projectID);
        Task<ListItemDTO<InvitationListItemDTO>> InvitationList(int userID, int page);
        Task<bool> AcceptInvitation(int userID, int invitationID, InvitationCheckStatuses acceptStatus)；
    }
}
