using AutoMapper;
using showdoc_server.Dtos.Request.Auth;
using showdoc_server.Dtos.Request.User;
using showdoc_server.Dtos.Table;

namespace showdoc_server.Profiles
{
    public class UsersProfile: Profile
    {
        public UsersProfile()
        {
            this.CreateMap<RegisterUserDTO, Users>();
            this.CreateMap<PassForgetDTO, Users>();
            this.CreateMap<Users, UserInfoDTO>();
        }
    }
}
