using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Request.Register;

namespace showdoc_server.Services.User
{
    public interface IUserService
    {
        Task<bool> Register(RegisterUserDTO entity);
    }
}
