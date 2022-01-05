using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using showdoc_server.Reponsitory.Sms;
using showdoc_server.Services.Cache.Redis;
using showdoc_server.Services.Http;
using showdoc_server.Utils;

namespace showdoc_server.Services.Sms
{
    public class SmsService : ISmsService
    {
        private readonly IRedisService redisService;
        private readonly IConfiguration configuration;
        private readonly IHttpService httpService;
        private readonly ISmsReponsitory smsReponsitory;

        public SmsService(IRedisService redisService,
            IConfiguration configuration,
            IHttpService httpService,
            ISmsReponsitory smsReponsitory)
        {
            this.redisService = redisService;
            this.configuration = configuration;
            this.httpService = httpService;
            this.smsReponsitory = smsReponsitory;
        }

        public async Task<bool> SendSmsCode(string prefix, string cellphone)
        {
            if (await this.smsReponsitory.Count(cellphone) >= 5)
            {
                throw new Exception("you send verify code more than 5 times");
            }
            string key = this.redisService.Key(prefix, cellphone);
            if (!string.IsNullOrEmpty(this.redisService.Get(key)))
            {
                return true;
            }
            string code = Code.GetCode();
            string text = $"您本次的验证码是{code}, 验证码于一分钟内有效，请及时使用。";

            string url = $"{configuration["Sms:Url"]}?Uid={configuration["Sms:Uid"]}&Key={configuration["Sms:Key"]}&smsMob={cellphone}&smsText={text}";
            string data = this.httpService.Get(url);
            bool isSuccess = data == "1";
            if (isSuccess)
            {
                this.redisService.Set(key, code, new TimeSpan(0, 1, 0));
            }
            await this.smsReponsitory.AddSms(new Dtos.Table.Sms()
            {
                Cellphone = cellphone,
                Content = text,
                Status = isSuccess,
                CreateTime = DateTime.Now,
            });
            return isSuccess;
        }
    }
}
