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

        public async Task<bool> DeleteFolderOrProjectAsync(int v, DeleteProjectOrFolderDTO entity)
        {
            if (entity.ObjectID == 0)
            {
                return true;
            }
            int cnt;
            if (entity.Type == ProjectListItemTypes.Folder)
            {
                cnt = await this.projectReponsitory.DeleteFolderAsync(v, entity);
            }
            else
            {
                cnt = await this.projectReponsitory.DeleteProjectAsync(v, entity);
            }

            return cnt > 0;
        }

        public async Task<IEnumerable<ProjectListItemDTO>> GetFolderContentAsync(int userID, int folderID)
        {
            IEnumerable<ProjectListItemDTO> folders = await this.projectReponsitory.GetFolderContentAsync(userID, folderID);

            return folders;
        }

        public async Task<bool> MoveFolderOrProjectAsync(int v, MoveProjectOrFolderDTO entity)
        {
            int cnt;
            if (entity.Type == ProjectListItemTypes.Folder)
            {
                cnt = await this.projectReponsitory.MoveFolderAsync(v, entity);
            }
            else
            {
                cnt = await this.projectReponsitory.MoveProjectAsync(v, entity);
            }

            return cnt > 0;
        }

        public async Task<bool> RenameFolderOrProjectAsync(int v, RenameProjectOrFolderDTO entity)
        {
            if (entity.ObjectID == 0)
            {
                return false;
            }
            int cnt;
            if (entity.Type == ProjectListItemTypes.Folder)
            {
                cnt = await this.projectReponsitory.RenameFolderAsync(v, entity);
            }
            else
            {
                cnt = await this.projectReponsitory.RenameProjectAsync(v, entity);
            }

            return cnt > 0;
        }
    }
}
