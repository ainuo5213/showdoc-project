using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Json;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HttpExtensions
    {
        public static async Task<IActionResult> SuccessAsync(this ControllerBase controllerBase, object data)
        {
            Result result = new Result(data);
            return await Task.FromResult(controllerBase.StatusCode(200, result));
        }
    }
}
