using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Invitation;
using showdoc_server.Reponsitory.Invitation;

namespace showdoc_server.Services.Invitation
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationReponsitory invitationReponsitory;

        public InvitationService(IInvitationReponsitory invitationReponsitory)
        {
            this.invitationReponsitory = invitationReponsitory;
        }

        public async Task<ListItemDTO<InvitationListItemDTO>> InvitationList(int userID, int page)
        {
            return await this.invitationReponsitory.InvitationList(userID, page);
        }

        public async Task<bool> InviteJoinProject(int userID, int invited, int projectID)
        {
            return await this.invitationReponsitory.InviteJoinProject(userID, invited, projectID) > 0;
        }

        public async Task<bool> JoinProject(int applyID, int projectID)
        {
            return await this.invitationReponsitory.JoinProject(applyID, projectID) > 0;
        }

        public async Task<bool> AcceptInvitation(int userID, int invitationID, InvitationCheckStatuses acceptStatus)
        {
            return await this.invitationReponsitory.AcceptInvitation(userID, invitationID, acceptStatus) > 0;
        }
    }
}
