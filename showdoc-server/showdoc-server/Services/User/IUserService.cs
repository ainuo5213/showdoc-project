using System.Threading.Tasks;
using showdoc_server.Dtos.Request.Auth;
using showdoc_server.Dtos.Request.User;

namespace showdoc_server.Services.User
{
    public interface IUserService
    {
        Task<bool> Register(RegisterUserDTO entity);
        Task<LoginResultDTO> Login(LoginUserDTO entity);
        Task<bool> PassForget(PassForgetDTO entity);
        Task<UserInfoDTO> GetUserByIdAsync(int userId);
    }
}
