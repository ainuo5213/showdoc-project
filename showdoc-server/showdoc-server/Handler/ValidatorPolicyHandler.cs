using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using showdoc_server.Dtos.Auth;
using showdoc_server.Services.Cache.Redis;

namespace showdoc_server.Handler
{
    public class ValidatorPolicyHandler : AuthorizationHandler<ValidatorRequirement>
    {
        private readonly IRedisService redisService;

        public ValidatorPolicyHandler(IRedisService redisService)
        {
            this.redisService = redisService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidatorRequirement requirement)
        {
            string id = context?.User?.Claims?.ToList()?.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("invalid user");
            }
            string key = this.redisService.Key("token", id);

            if (string.IsNullOrEmpty(this.redisService.Get(key)))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
