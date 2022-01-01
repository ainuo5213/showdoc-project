using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Context;
using showdoc_server.Dtos.Request.Project;
using showdoc_server.Dtos.Table;
using SqlSugar;

namespace showdoc_server.Reponsitory.Project
{
    public class ProjectReponsitory : IProjectReponsitory
    {
        public async Task<ProjectListItemDTO> CreateFolderAsync(int v, Folders folders)
        {
            bool isMine = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v);
            if (folders.ParentID != 0 && !isMine)
            {
                throw new Exception("no permission");
            }
            Folders insertEntity = await SugarContext.Context.Insertable(folders).ExecuteReturnEntityAsync();
            return new ProjectListItemDTO()
            {
                CreateTime = insertEntity.CreateTime,
                Name = insertEntity.Name,
                ObjectID = insertEntity.FolderID,
                ParentId = insertEntity.ParentID,
                SortTime = insertEntity.SortTime,
                Type = ProjectListItemTypes.Folder,
                UserID = v,
            };
        }

        public async Task<ProjectListItemDTO> CreateProjectAsync(int v, Projects projects)
        {
            bool isMine = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v);
            if (projects.FolderID != 0 && !isMine)
            {
                throw new Exception("no permission");
            }

            Projects insertEntity = await SugarContext.Context.Insertable(projects).ExecuteReturnEntityAsync();
            ProjectUsers projectUsers = new ProjectUsers()
            {
                UserID = v,
                ProjectID = insertEntity.ProjectID,
            };
            ProjectUsers insertEntity1 = await SugarContext.Context.Insertable(projectUsers).ExecuteReturnEntityAsync();

            return new ProjectListItemDTO()
            {
                CreateTime = insertEntity.CreateTime,
                Name = insertEntity.Name,
                ObjectID = insertEntity.ProjectID,
                ParentId = insertEntity.FolderID,
                SortTime = insertEntity1.SortTime,
                Type = ProjectListItemTypes.Project,
                UserID = v,
            };
        }

        public async Task<int> DeleteFolderAsync(int v, DeleteProjectOrFolderDTO entity)
        {
            bool isMine = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete);
            if (!isMine)
            {
                throw new Exception("no permission");
            }
            bool hasChildFolders = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.ParentID == entity.ObjectID);
            bool hasChildProjects = await SugarContext.Context.Queryable<Projects>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.ObjectID);
            if (hasChildFolders || hasChildProjects)
            {
                throw new Exception("folder is using. if you want to delete the folder, please delete the children at first");
            }
            return await SugarContext.Context.Deleteable<Folders>().Where(folder => folder.FolderID == entity.ObjectID && folder.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete).ExecuteCommandAsync();
        }

        public async Task<int> DeleteProjectAsync(int v, DeleteProjectOrFolderDTO entity)
        {
            bool isMine = await SugarContext.Context.Queryable<Projects>().AnyAsync(r => r.CreatorID == v && r.ProjectID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete);
            if (!isMine)
            {
                throw new Exception("no permission");
            }
            return await SugarContext.Context.Deleteable<Projects>().Where(project => project.ProjectID == entity.ObjectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete).ExecuteCommandAsync();
        }

        public async Task<IEnumerable<ProjectListItemDTO>> GetFolderContentAsync(int userID, int folderID)
        {
            IEnumerable<ProjectListItemDTO> projects = await SugarContext.Context.Queryable<Projects>()
                .LeftJoin<ProjectUsers>((project, projectUser) => project.ProjectID == projectUser.ProjectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && projectUser.UserID == userID)
                .Where(project => project.FolderID == folderID)
                .Select((project, projectUser) => new ProjectListItemDTO()
                {
                    UserID = projectUser.UserID,
                    CreateTime = project.CreateTime,
                    Name = project.Name,
                    ObjectID = project.ProjectID,
                    ParentId = project.FolderID,
                    SortTime = projectUser.SortTime,
                    Type = ProjectListItemTypes.Project,
                })
                .ToListAsync();

            IEnumerable<ProjectListItemDTO> folders = await SugarContext.Context.Queryable<Folders>()
                .Where(folder => folder.CreatorID == userID && folder.ParentID == folderID && folder.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && folder.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder)
                .Select(folder => new ProjectListItemDTO()
                {
                    CreateTime = folder.CreateTime,
                    SortTime = folder.SortTime,
                    Name = folder.Name,
                    ObjectID = folder.FolderID,
                    ParentId = folder.ParentID,
                    UserID = folder.CreatorID,
                    Type = ProjectListItemTypes.Folder,
                })
                .ToListAsync();

            return projects.Union(folders).OrderByDescending(r => r.SortTime);
        }

        public async Task<int> MoveFolderAsync(int v, MoveProjectOrFolderDTO entity)
        {
            if (entity.FolderID == entity.ObjectID)
            {
                throw new Exception("cannot move itself loop");
            }
            bool isMineofCurrentFolder = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder);
            bool isMineofTargetFolder = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.FolderID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder);
            if ((!isMineofCurrentFolder || !isMineofTargetFolder) && entity.FolderID != 0)
            {
                throw new Exception("no permission");
            }
            return await SugarContext.Context.Updateable<Folders>().SetColumns(r => r.ParentID == entity.FolderID).Where(r => r.FolderID == entity.ObjectID && r.CreatorID == v && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete).ExecuteCommandAsync();
        }

        public async Task<int> MoveProjectAsync(int v, MoveProjectOrFolderDTO entity)
        {
            bool isMineProject = await SugarContext.Context.Queryable<Projects>().AnyAsync(r => r.CreatorID == v && r.ProjectID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete);
            bool isMineFolder = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.FolderID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder);
            if ((!isMineProject || !isMineFolder) && entity.FolderID != 0)
            {
                throw new Exception("no permission");
            }
            return await SugarContext.Context.Updateable<Projects>().SetColumns(r => r.FolderID == entity.FolderID).Where(r => r.ProjectID == entity.ObjectID && r.CreatorID == v && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete).ExecuteCommandAsync();
        }
    }
}
