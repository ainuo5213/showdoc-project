using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using showdoc_server.Dtos.Request.Auth;
using showdoc_server.Dtos.Request.User;
using showdoc_server.Dtos.Table;
using showdoc_server.Reponsitory.User;
using showdoc_server.Services.Cache.Redis;

namespace showdoc_server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserReponsitory userReponsitory;
        private readonly IMapper mapper;
        private readonly IRedisService redisService;
        private readonly IConfiguration configuration;

        public UserService(IUserReponsitory userReponsitory, IMapper mapper, IConfiguration configuration, IRedisService redisService)
        {
            this.userReponsitory = userReponsitory;
            this.mapper = mapper;
            this.redisService = redisService;
            this.configuration = configuration;
        }

        public async Task<bool> Register(RegisterUserDTO entity)
        {
            Users user = this.mapper.Map<Users>(entity);
            int cnt = await this.userReponsitory.AddUserAsync(user);
            return cnt > 0;
        }

        public async Task<LoginResultDTO> Login(LoginUserDTO entity)
        {
            Users user = await this.userReponsitory.GetUserByPhoneAsync(entity.Cellphone);
            if (user == null)
            {
                throw new Exception("user is not register");
            }
            if (MD5Hash.Hash.Content(entity.Password) != user.Password)
            {
                throw new Exception("user password is not currect");
            }
            string key = this.redisService.Key("token", user.UserID.ToString());
            string token = this.redisService.Get(key);
            if (string.IsNullOrEmpty(token))
            {
                token = GenToken(user.UserID, user.UserID.ToString());
            }
            this.redisService.Set(key, token, TimeSpan.FromDays(30));
            return new LoginResultDTO()
            {
                HeadImg = user.HeadImg,
                Username = user.Username,
                UserID = user.UserID,
                Token = token,
            };
        }

        private string GenToken(int userId, string username)
        {
            // header
            string algorithm = SecurityAlgorithms.HmacSha256;

            // payload
            var claims = new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                };

            // signiture
            var secretByte = Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]);

            // 得到secretKey
            var signingKey = new SymmetricSecurityKey(secretByte);

            // 初始化授权对象
            var signingCredentials = new SigningCredentials(signingKey, algorithm);

            // 得到token
            var token = new JwtSecurityToken(
                issuer: configuration["Authentication:Issure"],
                audience: configuration["Authentication:Audience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        public async Task<bool> PassForget(PassForgetDTO entity)
        {
            Users user = this.mapper.Map<Users>(entity);
            int cnt = await this.userReponsitory.ChangePasswordAsync(user);
            return cnt > 0;
        }

        public async Task<UserInfoDTO> GetUserByIdAsync(int userId)
        {
            Users user = await this.userReponsitory.GetUserByIdAsync(userId);
            UserInfoDTO data = this.mapper.Map<UserInfoDTO>(user);
            return data;
        }
    }
}
