using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using showdoc_server.Dtos.Request.Register;
using showdoc_server.Dtos.Table;

namespace showdoc_server.Profiles
{
    public class UsersProfile: Profile
    {
        public UsersProfile()
        {
            this.CreateMap<RegisterUserDTO, Users>();
        }
    }
}
