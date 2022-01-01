using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using showdoc_server.Dtos.Request.Sms;
using showdoc_server.Services.Sms;

namespace showdoc_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISmsService smsService;

        public AuthController(ISmsService smsService)
        {
            this.smsService = smsService;
        }

        [HttpPost("message")]
        public async Task<IActionResult> Message([FromBody] SmsDTO entity)
        {
            if (entity.Type != SmsTypes.ForgetPassword && entity.Type != SmsTypes.Register)
            {
                throw new ArgumentException("type is not supported");
            }
            bool isSuccess = await this.smsService.SendSmsCode(entity.Type.ToString(), entity.Cellphone);
            return await this.SuccessAsync(isSuccess);
        }
    }
}
