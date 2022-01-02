using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Context;
using showdoc_server.Dtos.Json;
using showdoc_server.Dtos.Request.Invitation;
using showdoc_server.Dtos.Table;
using SqlSugar;

namespace showdoc_server.Reponsitory.Invitation
{
    public class InvitationReponsitory : IInvitationReponsitory
    {
        public async Task<ListItemDTO<InvitationListItemDTO>> InvitationList(int userID, int page)
        {
            ISugarQueryable<InvitationListItemDTO> projectInvitations = SugarContext.Context.Queryable<ProjectInvitations>()
                .Where(projectInvitation => projectInvitation.CheckUserID == userID)
                .InnerJoin<Projects>((projectInvitation, project) => project.ProjectID == projectInvitation.ProjectID)
                .InnerJoin<Users>((projectInvitation, project, applyUser) => applyUser.UserID == projectInvitation.UserID)
                .Select((projectInvitation, project, applyUser) => new InvitationListItemDTO()
                {
                    ProjectID = projectInvitation.ProjectID,
                    CheckTime = projectInvitation.CheckTime,
                    CreateTime = projectInvitation.CreateTime,
                    InvitationID = projectInvitation.ProjectInvitationID,
                    Invited = projectInvitation.UserID,
                    InvitedUsername = applyUser.Username,
                    ProjectName = project.Name,
                    Status = projectInvitation.Status,
                });
            int totalCount = await projectInvitations.CountAsync();
            IEnumerable<InvitationListItemDTO> pageData = await projectInvitations.OrderBy(r => r.CreateTime).ToPageListAsync(page, 20);

            return new ListItemDTO<InvitationListItemDTO>()
            {
                Items = pageData,
                TotalCount = totalCount,
            };
        }

        public async Task<int> AcceptInvitation(int userID, int invitationID, InvitationCheckStatuses acceptStatus)
        {
            if (acceptStatus == InvitationCheckStatuses.Checking)
            {
                throw new Exception("invation status invalid");
            }

            ProjectInvitations invitation = await SugarContext.Context.Queryable<ProjectInvitations>()
                .FirstAsync(projectInvitation => projectInvitation.ProjectInvitationID == invitationID && projectInvitation.CheckUserID == userID && projectInvitation.Status == InvitationCheckStatuses.Checking);
            if (invitation == null)
            {
                throw new Exception("no available invitation");
            }

            await SugarContext.Context.Updateable<ProjectInvitations>()
                .SetColumns(projectInvitation => new ProjectInvitations()
                {
                    CheckTime = DateTime.Now,
                    Status = acceptStatus,
                    UpdateTime = DateTime.Now,
                })
               .Where(projectInvitation => projectInvitation.ProjectInvitationID == invitationID && projectInvitation.CheckUserID == userID && projectInvitation.Status == InvitationCheckStatuses.Checking)
               .ExecuteCommandAsync();
           
            if (acceptStatus == InvitationCheckStatuses.Passed)
            {
                SugarContext.Context.Insertable(new ProjectFolders()
                {
                    CreateTime = DateTime.Now,
                    FolderID = 0,
                    ProjectID = invitation.ProjectID,
                    UpdateTime = DateTime.Now,
                    UserID = invitation.UserID,
                });
            }

            return 1;
        }

        public async Task<int> InviteJoinProject(int invite, int invited, int projectID)
        {
            Projects project = await SugarContext.Context.Queryable<Projects>()
                .FirstAsync(project => project.ProjectID == projectID && project.DeleteStatus == DeleteStatuses.UnDelete);
            if (project == null)
            {
                throw new Exception("project has been deleted");
            }

            // 加入某个项目只有没有申请加入过或加入拒绝状态或已申请过但人已经离开了项目才允许加入

            // 1. 是否加入了项目
            bool joinedProject = await SugarContext.Context.Queryable<ProjectUsers>()
                .AnyAsync(projectUser => projectUser.ProjectID == projectID && projectUser.UserID == invited);

            // 2. 是否申请过或状态不拒绝状态
            bool invitedTo = await SugarContext.Context.Queryable<ProjectInvitations>().AnyAsync(projectInvitation => projectInvitation.UserID == invited && projectInvitation.ProjectID == projectID && projectInvitation.Status != InvitationCheckStatuses.Rejected);

            // 如果已经加入了项目，则不允许再邀请
            if (joinedProject)
            {
                throw new Exception("user has joined project");
            }

            // 没有加入项目，而且已经邀请了的话，但邀请未处理，则不允许再邀请一次
            // TODO：搜索加入项目和人的时候需要将邀请列表中未处理的邀请函的项目和审核人除外
            if (!joinedProject && invitedTo)
            {
                throw new Exception("cannot invite user before apply checked");
            }

            return await SugarContext.Context.Insertable(new ProjectInvitations()
            {
                UserID = invited,
                CheckTime = new DateTime(1900, 1, 1),
                CheckUserID = invited,
                InviteByID = invite,
                CreateTime = DateTime.Now,
                ProjectID = projectID,
                Status = InvitationCheckStatuses.Checking,
                UpdateTime = DateTime.Now,
            }).ExecuteCommandAsync();
        }

        public async Task<int> JoinProject(int applyID, int projectID)
        {
            Projects project = await SugarContext.Context.Queryable<Projects>()
                .FirstAsync(project => project.ProjectID == projectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete);
            if (project == null)
            {
                throw new Exception("project has been deleted");
            }

            // 加入某个项目只有没有申请加入过或加入拒绝状态或已申请过但人已经离开了项目才允许加入

            // 1. 是否加入了项目
            bool joinedProject = await SugarContext.Context.Queryable<ProjectUsers>()
                .AnyAsync(projectUser => projectUser.ProjectID == projectID && projectUser.UserID == applyID);

            // 2. 是否申请过或状态不拒绝状态
            bool invitedTo = await SugarContext.Context.Queryable<ProjectInvitations>().AnyAsync(projectInvitation => projectInvitation.UserID == applyID && projectInvitation.ProjectID == projectID && projectInvitation.Status != Dtos.Request.Invitation.InvitationCheckStatuses.Rejected);

            // 如果已经加入了项目，则不允许再邀请
            if (joinedProject)
            {
                throw new Exception("user has joined project");
            }

            // 没有加入项目，而且已经邀请了的话，但邀请未处理，则不允许再邀请一次
            // TODO：搜索加入项目和人的时候需要将邀请列表中未处理的邀请函的项目和审核人除外
            if (!joinedProject && invitedTo)
            {
                throw new Exception("cannot invite user before apply checked");
            }

            return await SugarContext.Context.Insertable(new ProjectInvitations()
            {
                UserID = applyID,
                CheckTime = new DateTime(1900, 1, 1),
                CheckUserID = project.CreatorID,
                InviteByID = applyID,
                CreateTime = DateTime.Now,
                ProjectID = projectID,
                Status = Dtos.Request.Invitation.InvitationCheckStatuses.Checking,
                UpdateTime = DateTime.Now,
            }).ExecuteCommandAsync();
        }
    }
}
