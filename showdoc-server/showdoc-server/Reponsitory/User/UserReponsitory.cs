using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Context;
using showdoc_server.Dtos.Table;

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

        public async Task<Users> GetUserByPhoneAsync(string cellphone)
        {
            return await SugarContext.Context.Queryable<Users>().FirstAsync(r => r.Cellphone == cellphone);
        }
    }
}
