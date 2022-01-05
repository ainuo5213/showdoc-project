using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using showdoc_server.Context;
using showdoc_server.Dtos.Request.Project;
using showdoc_server.Dtos.Table;
using SqlSugar;

namespace showdoc_server.Reponsitory.Project
{
    public class ProjectReponsitory : IProjectReponsitory
    {
        public async Task<ProjectListItemDTO> CreateFolderAsync(int v, CreateProjectOrFolderDTO entity)
        {
            // 查看创建文件夹所在的文件夹是否是登陆者自己的文件夹
            bool isMine = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.FolderID);
            if (entity.FolderID != 0 && !isMine)
            {
                throw new Exception("暂无权限");
            }
            Folders insertEntity = await SugarContext.Context.Insertable(new Folders()
            {
                CreatorID = v,
                CreateTime = DateTime.Now,
                DeleteStatus = Dtos.Json.DeleteStatuses.UnDelete,
                Name = entity.Name,
                ParentID = entity.FolderID,
                ProjectID = 0,
                SortTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Type = Dtos.Request.Folder.FolderTypes.ProjectFolder
            }).ExecuteReturnEntityAsync();
            return new ProjectListItemDTO()
            {
                CreateTime = insertEntity.CreateTime,
                Name = insertEntity.Name,
                ObjectID = insertEntity.FolderID,
                ParentID = insertEntity.ParentID,
                SortTime = insertEntity.SortTime,
                Type = ProjectListItemTypes.Folder,
                UserID = insertEntity.CreatorID,
            };
        }

        public async Task<ProjectListItemDTO> CreateProjectAsync(int v, CreateProjectOrFolderDTO entity)
        {
            // 查看创建文件夹所在的文件夹是否是登陆者自己的文件夹
            bool isMine = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.FolderID);
            if (entity.FolderID != 0 && !isMine)
            {
                throw new Exception("暂无权限");
            }

            // 插入项目（项目不具有文件夹，每个人都可以为项目分配文件夹）
            Projects insertEntity = await SugarContext.Context.Insertable(new Projects()
            {
                CreatorID = v,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now,
                DeleteStatus = Dtos.Json.DeleteStatuses.UnDelete,
                Name = entity.Name,
            }).ExecuteReturnEntityAsync();

            // 项目成员表
            ProjectUsers projectUsers = new ProjectUsers()
            {
                UserID = v,
                ProjectID = insertEntity.ProjectID,
                CreateTime = DateTime.Now,
                SortTime = DateTime.Now
            };

            // 插入项目所在的个人文件夹
            ProjectFolders projectFolders = new ProjectFolders()
            {
                UserID = v,
                ProjectID = insertEntity.ProjectID,
                FolderID = entity.FolderID,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };


            ProjectUsers insertEntity1 = await SugarContext.Context.Insertable(projectUsers).ExecuteReturnEntityAsync();
            await SugarContext.Context.Insertable(projectFolders).ExecuteCommandAsync();

            return new ProjectListItemDTO()
            {
                CreateTime = insertEntity.CreateTime,
                Name = insertEntity.Name,
                ObjectID = insertEntity.ProjectID,
                ParentID = entity.FolderID,
                SortTime = insertEntity1.SortTime,
                Type = ProjectListItemTypes.Project,
                UserID = v,
            };
        }

        public async Task<int> DeleteFolderAsync(int v, DeleteProjectOrFolderDTO entity)
        {
            // 查看删除的文件夹所在是否是登陆者自己的文件夹
            bool isMine = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder);
            if (!isMine)
            {
                throw new Exception("暂无权限");
            }

            // 所删除文件夹的子文件夹
            ISugarQueryable<Folders> folderQuaryable = SugarContext.Context.Queryable<Folders>().Where(r => r.CreatorID == v && r.ParentID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder);

            // 查询参与的未删除的项目
            ISugarQueryable<Projects> projectQuaryable = SugarContext.Context.Queryable<Projects>().Where(r => r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete)
                                                        .InnerJoin<ProjectUsers>((project, projectUser) => project.ProjectID == projectUser.ProjectID && projectUser.UserID == v)
                                                        .Select((project, projectUser) => project);

            // 查询参与的未删除的项目中在所用户要删除的文件夹下的项目
            projectQuaryable = SugarContext.Context.Queryable(projectQuaryable)
                                                            .InnerJoin<ProjectFolders>((project, projectFolder) => project.ProjectID == projectFolder.ProjectID && projectFolder.UserID == v && projectFolder.FolderID == entity.ObjectID)
                                                            .Select((project, projectFolder) => project);
            bool hasChildFolders = folderQuaryable.Count() > 0;
            bool hasChildProjects = projectQuaryable.Count() > 0;
            if (hasChildFolders || hasChildProjects)
            {
                throw new Exception("文件夹正在使用");
            }

            return await SugarContext.Context.Updateable<Folders>()
                         .SetColumns(r => new Folders()
                         {
                             DeleteStatus = Dtos.Json.DeleteStatuses.Deleted,
                             UpdateTime = DateTime.Now,
                         })
                         .Where(folder => folder.CreatorID == v && folder.FolderID == entity.ObjectID && folder.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && folder.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder)
                         .ExecuteCommandAsync();
        }

        public async Task<int> DeleteProjectAsync(int v, DeleteProjectOrFolderDTO entity)
        {
            // 当前用户文件夹下的这个项目删除，即删除项目所对应当前用的projectFolder
            ISugarQueryable<ProjectFolders> projects = SugarContext.Context.Queryable<ProjectFolders>()
                          .InnerJoin<Projects>((prjectFolder, project) => prjectFolder.ProjectID == project.ProjectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && prjectFolder.UserID == v && prjectFolder.ProjectID == entity.ObjectID)
                          .Select((prjectFolder, project) => prjectFolder);
            int line = await SugarContext.Context.Deleteable(projects.ToList()).ExecuteCommandAsync();

            // 如果没有其他人有这个项目了，将这个项目删除
            int restCount = SugarContext.Context.Queryable<ProjectFolders>().Count(r => r.ProjectID == entity.ObjectID);
            if (restCount == 0)
            {
                line += await SugarContext.Context.Updateable<Projects>()
                    .SetColumns(r => new Projects()
                    {
                        DeleteStatus = Dtos.Json.DeleteStatuses.Deleted,
                        UpdateTime = DateTime.Now
                    }).Where(project => project.ProjectID == entity.ObjectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete).ExecuteCommandAsync();
            }

            return line;
        }

        public async Task<IEnumerable<ProjectListItemDTO>> GetFolderContentAsync(int userID, int folderID)
        {
            // 查找当前用户的folder父文件夹ID和projectFolder文件夹ID是所查询的文件夹ID
            IEnumerable<ProjectListItemDTO> projects = await SugarContext.Context.Queryable<Projects>()
                .Where(r => r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((project, projectUser) => project.ProjectID == projectUser.ProjectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && projectUser.UserID == userID)
                .InnerJoin<ProjectFolders>((project, projectUser, projectFolder) => project.ProjectID == projectFolder.ProjectID && projectUser.UserID == userID && projectFolder.FolderID == folderID)
                .Select((project, projectUser, projectFolder) => new ProjectListItemDTO()
                {
                    UserID = projectUser.UserID,
                    CreateTime = project.CreateTime,
                    Name = project.Name,
                    ObjectID = project.ProjectID,
                    ParentID = projectFolder.FolderID,
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
                    ParentID = folder.ParentID,
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
                throw new Exception("移动文件非法");
            }

            Expression<Func<Folders, bool>> condition = r => r.CreatorID == v && r.FolderID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder;
            bool isMineofCurrentFolder = await SugarContext.Context.Queryable<Folders>().AnyAsync(condition);
            bool isMineofTargetFolder = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.FolderID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder);
            if ((!isMineofCurrentFolder || !isMineofTargetFolder) && entity.FolderID != 0)
            {
                throw new Exception("暂无权限");
            }
            return await SugarContext.Context.Updateable<Folders>().SetColumns(r => new Folders()
            {
                ParentID = entity.FolderID,
                UpdateTime = DateTime.Now,
            }).Where(condition).ExecuteCommandAsync();
        }

        public async Task<int> MoveProjectAsync(int v, MoveProjectOrFolderDTO entity)
        {
            ISugarQueryable<ProjectFolders> projectFolders = SugarContext.Context.Queryable<ProjectFolders>().Where(r => r.UserID == v)
                                                             .InnerJoin<Projects>((projectFolder, project) => projectFolder.ProjectID == project.ProjectID && project.ProjectID == entity.ObjectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete);

            bool isMineProject = projectFolders.Count() > 0;
            bool isMineFolder = await SugarContext.Context.Queryable<Folders>().AnyAsync(r => r.CreatorID == v && r.FolderID == entity.FolderID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder);
            if ((!isMineProject || !isMineFolder) && entity.FolderID != 0)
            {
                throw new Exception("暂无权限");
            }
            return await SugarContext.Context.Updateable<ProjectFolders>().SetColumns(r => new ProjectFolders()
            {
                FolderID = entity.FolderID,
                UpdateTime = DateTime.Now,
            }).Where(r => r.ProjectID == entity.ObjectID && r.UserID == v).ExecuteCommandAsync();
        }

        public async Task<int> RenameFolderAsync(int v, RenameProjectOrFolderDTO entity)
        {
            Expression<Func<Folders, bool>> condition = r => r.CreatorID == v && r.FolderID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete && r.Type == Dtos.Request.Folder.FolderTypes.ProjectFolder;
            bool isMineFolder = await SugarContext.Context.Queryable<Folders>().AnyAsync(condition);
            if (!isMineFolder)
            {
                throw new Exception("暂无权限");
            }
            return await SugarContext.Context.Updateable<Folders>().SetColumns(r => new Folders()
            {
                Name = entity.Name,
                UpdateTime = DateTime.Now,
            }).Where(condition).ExecuteCommandAsync();
        }

        public async Task<int> RenameProjectAsync(int v, RenameProjectOrFolderDTO entity)
        {
            Expression<Func<Projects, bool>> condition = r => r.CreatorID == v && r.ProjectID == entity.ObjectID && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete;
            bool isMineProject = await SugarContext.Context.Queryable<Projects>().AnyAsync(condition);
            if (!isMineProject)
            {
                throw new Exception("暂无权限");
            }
            return await SugarContext.Context.Updateable<Projects>().SetColumns(r => new Projects()
            {
                Name = entity.Name,
                UpdateTime = DateTime.Now,
            }).Where(condition).ExecuteCommandAsync();
        }

        public async Task<IEnumerable<SearchProjectItemDTO>> SearchProjectByKeyAsync(int v, string key)
        {
            return await SugarContext.Context.Queryable<Projects>()
                .Where(r => r.Name.Contains(key) && r.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete)
                .InnerJoin<ProjectUsers>((project, projectUser) => project.ProjectID == projectUser.ProjectID && projectUser.UserID != v)
                .InnerJoin<Users>((project, projectUser, _user) => projectUser.UserID == _user.UserID)
                .OrderBy((project, projectUser, _user) => project.Name)
                .Select((project, projectUser, _user) => new SearchProjectItemDTO()
                {
                    ProjectID = project.ProjectID,
                    Creator = _user.Username,
                    ProjectName = project.Name,
                })
                .Take(5)
                .ToListAsync();
        }
    }
}
