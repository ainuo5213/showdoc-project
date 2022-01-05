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
        public async Task<IActionResult> List([FromQuery] int folderID)
        {
            IEnumerable<ProjectListItemDTO> data = await this.projectService.GetFolderContentAsync(this.GetUserID(), folderID);
            return await this.SuccessAsync(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProjectOrFolderDTO entity)
        {
            if (entity.Type != ProjectListItemTypes.Folder && entity.Type != ProjectListItemTypes.Project)
            {
                throw new System.Exception("不支持的文件类型");
            }
            ProjectListItemDTO data = await this.projectService.CreateFolderOrProjectAsync(this.GetUserID(), entity);
            return await this.SuccessAsync(data);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProjectOrFolderDTO entity)
        {
            if (entity.Type != ProjectListItemTypes.Folder && entity.Type != ProjectListItemTypes.Project)
            {
                throw new System.Exception("不支持的文件类型");
            }
            bool data = await this.projectService.DeleteFolderOrProjectAsync(this.GetUserID(), entity);
            return await this.SuccessAsync(data);
        }

        [HttpPost("move")]
        public async Task<IActionResult> Move([FromBody] MoveProjectOrFolderDTO entity)
        {
            if (entity.Type != ProjectListItemTypes.Folder && entity.Type != ProjectListItemTypes.Project)
            {
                throw new System.Exception("不支持的文件类型");
            }
            bool data = await this.projectService.MoveFolderOrProjectAsync(this.GetUserID(), entity);
            return await this.SuccessAsync(data);
        }

        [HttpPost("rename")]
        public async Task<IActionResult> Rename([FromBody] RenameProjectOrFolderDTO entity)
        {
            if (entity.Type != ProjectListItemTypes.Folder && entity.Type != ProjectListItemTypes.Project)
            {
                throw new System.Exception("不支持的文件类型");
            }
            bool data = await this.projectService.RenameFolderOrProjectAsync(this.GetUserID(), entity);
            return await this.SuccessAsync(data);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string key)
        {
            IEnumerable<SearchProjectItemDTO> data = await this.projectService.SearchProjectByKeyAsync(this.GetUserID(), key);
            return await this.SuccessAsync(data);
        }
    }
}
