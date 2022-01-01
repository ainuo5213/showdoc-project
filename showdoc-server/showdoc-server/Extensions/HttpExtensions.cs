using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HttpExtensions
    {
        public static int GetUserID(this ControllerBase controllerBase)
        {
            Claim claim = controllerBase.User.Claims.ToList().FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                throw new Exception("invalid user");
            }
            return Convert.ToInt32(claim.Value);
        }
        public static async Task<IActionResult> SuccessAsync(this ControllerBase controllerBase, object data)
        {
            Result result = new Result(data);
            return await Task.FromResult(controllerBase.StatusCode(200, result));
        }
    }
}
