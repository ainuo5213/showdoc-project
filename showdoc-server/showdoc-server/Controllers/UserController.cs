using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using showdoc_server.Attributes;
using showdoc_server.Dtos.Request.Auth;
using showdoc_server.Dtos.Request.User;
using showdoc_server.Services.Cache.Redis;
using showdoc_server.Services.Sms;
using showdoc_server.Services.User;

namespace showdoc_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ShowdocAuthorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("info")]
        public async Task<IActionResult> UserInfo()
        {
            UserInfoDTO data = await this.userService.GetUserByIdAsync(this.GetUserID());
            return await this.SuccessAsync(data);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> UserInfo([FromServices] IRedisService redisService)
        {
            int userId = this.GetUserID();
            string key = redisService.Key("token", userId.ToString());
            if (!string.IsNullOrEmpty(redisService.Get(key)))
            {
                redisService.Delete(key);
            }
            return await this.SuccessAsync(true);
        }
    }
}
