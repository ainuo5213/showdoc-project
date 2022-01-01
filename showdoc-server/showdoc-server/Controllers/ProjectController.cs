using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using showdoc_server.Attributes;
using showdoc_server.Dtos.Request.Project;
using showdoc_server.Dtos.Request.User;
using showdoc_server.Services.Cache.Redis;
using showdoc_server.Services.Project;
using showdoc_server.Services.User;

namespace showdoc_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ShowdocAuthorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List(int folderID)
        {
            IEnumerable<ProjectListItemDTO> data = await this.projectService.GetFolderContentAsync(this.GetUserID(), folderID);
            return await this.SuccessAsync(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateProjectOrFolderDTO entity)
        {
            if (entity.Type != ProjectListItemTypes.Folder && entity.Type != ProjectListItemTypes.Project)
            {
                throw new System.Exception("not supported object type");
            }
            ProjectListItemDTO data = await this.projectService.CreateFolderOrProjectAsync(this.GetUserID(), entity);
            return await this.SuccessAsync(data);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(DeleteProjectOrFolderDTO entity)
        {
            if (entity.Type != ProjectListItemTypes.Folder && entity.Type != ProjectListItemTypes.Project)
            {
                throw new System.Exception("not supported object type");
            }
            bool data = await this.projectService.DeleteFolderOrProjectAsync(this.GetUserID(), entity);
            return await this.SuccessAsync(data);
        }

        [HttpPost("move")]
        public async Task<IActionResult> Move(MoveProjectOrFolderDTO entity)
        {
            if (entity.Type != ProjectListItemTypes.Folder && entity.Type != ProjectListItemTypes.Project)
            {
                throw new System.Exception("not supported object type");
            }
            bool data = await this.projectService.MoveFolderOrProjectAsync(this.GetUserID(), entity);
            return await this.SuccessAsync(data);
        }
    }
}
