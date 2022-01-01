using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Context;
using showdoc_server.Dtos.Request.User;
using showdoc_server.Dtos.Table;
using SqlSugar;

namespace showdoc_server.Reponsitory.User
{
    public class UserReponsitory : IUserReponsitory
    {
        public async Task<int> AddUserAsync(Users user)
        {
            Users entity = await this.GetUserByPhoneAsync(user.Cellphone);
            if (entity != null)
            {
                throw new Exception("user already registered");
            }
            return await SugarContext.Context.Insertable(user).ExecuteCommandAsync();
        }

        public async Task<int> ChangePasswordAsync(Users user)
        {
            Users entity = await this.GetUserByPhoneAsync(user.Cellphone);
            if (entity == null)
            {
                throw new Exception("user not register");
            }

            return await SugarContext.Context.Updateable<Users>().SetColumns(r => r.Password == user.Password).Where(r => r.UserID == entity.UserID).ExecuteCommandAsync();
        }

        public async Task<Users> GetUserByIdAsync(int userId)
        {
            return await SugarContext.Context.Queryable<Users>().FirstAsync(r => r.UserID == userId);
        }

        public async Task<Users> GetUserByPhoneAsync(string cellphone)
        {
            return await SugarContext.Context.Queryable<Users>().FirstAsync(r => r.Cellphone == cellphone);
        }

        public async Task<IEnumerable<SearchUserJoinProjectItemDTO>> SearchUserByKeyAsync(int userId, int projectID, string key)
        {
            return await SugarContext.Context.Queryable<Users>()
                .LeftJoin<ProjectUsers>((_user, projectUser) => _user.UserID == projectUser.UserID && projectUser.ProjectID == projectID)
                .LeftJoin<Projects>((_user, projectUser, project) => projectUser.ProjectID == project.ProjectID && project.DeleteStatus == Dtos.Json.DeleteStatuses.UnDelete)
                .Where((_user, projectUser, project) => SqlFunc.IsNullOrEmpty(projectUser.ProjectUserID) && _user.UserID != userId)
                .WhereIF(!string.IsNullOrEmpty(key), _user => _user.Username.Contains(key))
                .Select((_user, projectUser, project) => new SearchUserJoinProjectItemDTO()
                {
                    UserID = _user.UserID,
                    UserName = _user.Username,
                    HeadImg = _user.HeadImg,
                })
                .OrderBy(_user => _user.UserName)
                .Take(5)
                .ToListAsync();

        }
    }
}
