using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Request.Project;
using showdoc_server.Dtos.Table;
using showdoc_server.Reponsitory.Project;

namespace showdoc_server.Services.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectReponsitory projectReponsitory;

        public ProjectService(IProjectReponsitory projectReponsitory)
        {
            this.projectReponsitory = projectReponsitory;
        }

        public async Task<ProjectListItemDTO> CreateFolderOrProjectAsync(int v, CreateProjectOrFolderDTO entity)
        {
            if (entity.Type == ProjectListItemTypes.Project)
            {
                return await this.projectReponsitory.CreateProjectAsync(v, new Projects()
                {
                    FolderID = entity.FolderID,
                    CreatorID = v,
                    Name = entity.Name,
                });
            }
            else
            {
                return await this.projectReponsitory.CreateFolderAsync(v, new Folders()
                {
                    CreatorID = v,
                    Name = entity.Name,
                    ParentID = entity.FolderID,
                    Type = Dtos.Request.Folder.FolderTypes.ProjectFolder,
                });
            }
        }

        public async Task<IEnumerable<ProjectListItemDTO>> GetFolderContentAsync(int userID, int folderID)
        {
            IEnumerable<ProjectListItemDTO> folders = await this.projectReponsitory.GetFolderContentAsync(userID, folderID);

            return folders;
        }
    }
}
