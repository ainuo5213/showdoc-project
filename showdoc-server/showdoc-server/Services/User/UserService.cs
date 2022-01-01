using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using showdoc_server.Dtos.Request.Register;
using showdoc_server.Dtos.Table;
using showdoc_server.Reponsitory.User;

namespace showdoc_server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserReponsitory userReponsitory;
        private readonly IMapper mapper;

        public UserService(IUserReponsitory userReponsitory, IMapper mapper)
        {
            this.userReponsitory = userReponsitory;
            this.mapper = mapper;
        }

        public async Task<bool> Register(RegisterUserDTO entity)
        {
            Users user = this.mapper.Map<Users>(entity);
            int cnt = await this.userReponsitory.AddUserAsync(user);
            return cnt > 0;
        }
    }
}
