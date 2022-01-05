using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using showdoc_server.Dtos.Request.Auth;
using showdoc_server.Services.Cache.Redis;
using showdoc_server.Services.Sms;
using showdoc_server.Services.User;

namespace showdoc_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISmsService smsService;
        private readonly IUserService userService;

        public AuthController(ISmsService smsService, IUserService userService)
        {
            this.smsService = smsService;
            this.userService = userService;
        }

        [HttpPost("message")]
        public async Task<IActionResult> Message([FromBody] SmsDTO entity)
        {
            if (entity.Type != SmsTypes.ForgetPassword && entity.Type != SmsTypes.Register)
            {
                throw new ArgumentException("短信类型不被支持");
            }
            bool isSuccess = await this.smsService.SendSmsCode(entity.Type.ToString(), entity.Cellphone);
            return await this.SuccessAsync(isSuccess);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromServices] IRedisService redisService, [FromBody] RegisterUserDTO entity)
        {
            string key = redisService.Key(SmsTypes.Register.ToString(), entity.Cellphone);
            if (string.IsNullOrEmpty(redisService.Get(key)) || redisService.Get(key) != entity.VerifyCode)
            {
                throw new ArgumentException("验证码不正确");
            }
            entity.Password = MD5Hash.Hash.Content(entity.Password);
            bool isSuccess = await this.userService.Register(entity);
            return await this.SuccessAsync(isSuccess);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO entity)
        {
            LoginResultDTO userDTO = await this.userService.Login(entity);
            return await this.SuccessAsync(userDTO);
        }

        [HttpPost("passforget")]
        public async Task<IActionResult> PassForget([FromServices] IRedisService redisService, [FromBody] PassForgetDTO entity)
        {
            string key = redisService.Key(SmsTypes.ForgetPassword.ToString(), entity.Cellphone);
            if (string.IsNullOrEmpty(redisService.Get(key)) || redisService.Get(key) != entity.VerifyCode)
            {
                throw new ArgumentException("验证码不正确");
            }
            entity.Password = MD5Hash.Hash.Content(entity.Password);
            bool isSuccess = await this.userService.PassForget(entity);
            return await this.SuccessAsync(isSuccess);
        }
    }
}
