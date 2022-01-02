using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using showdoc_server.Attributes;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Invitation;
using showdoc_server.Services.Invitation;

namespace showdoc_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ShowdocAuthorize]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService invitationService;

        public InvitationController(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        [HttpPost("invite")]
        public async Task<IActionResult> Invite([FromBody] InvitationPossitiveDTO entity)
        {
            if (entity.ProjectID == 0)
            {
                throw new Exception("project is not available");
            }
            int invite = this.GetUserID();
            bool data = await this.invitationService.InviteJoinProject(invite, entity.Invited, entity.ProjectID);

            return await this.SuccessAsync(data);
        }

        [HttpPost("join")]
        public async Task<IActionResult> Invite([FromBody] InvitationPassiveDTO entity)
        {
            if (entity.ProjectID == 0)
            {
                throw new Exception("project is not available");
            }
            int applyID = this.GetUserID();
            bool data = await this.invitationService.JoinProject(applyID, entity.ProjectID);

            return await this.SuccessAsync(data);
        }

        [HttpGet("list")]
        public async Task<IActionResult> InvitationList([FromQuery] int page = 1)
        {
            int userID = this.GetUserID();
            ListItemDTO<InvitationListItemDTO> data = await this.invitationService.InvitationList(userID, page);

            return await this.SuccessAsync(data);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptInvitation([FromBody] AcceptInvitationDTO entity)
        {
            int userID = this.GetUserID();
            bool data = await this.invitationService.AcceptInvitation(userID, entity.InvitationID, entity.Status);

            return await this.SuccessAsync(data);
        }
    }
}
