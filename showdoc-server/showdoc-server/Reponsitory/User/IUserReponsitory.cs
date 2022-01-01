using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Table;

namespace showdoc_server.Reponsitory.User
{
    public interface IUserReponsitory
    {
        Task<int> AddUserAsync(Users user);
        Task<Users> GetUserByPhoneAsync(string cellphone);
        Task<int> ChangePasswordAsync(Users user);
    }
}
