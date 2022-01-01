using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using showdoc_server.Dtos.Json;

namespace showdoc_server.Filters
{
    public class ExceptionLogFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.Result = new JsonResult(new Result(null) { Errmsg = context.Exception.Message, Errno = Dtos.Enums.Errno.Failed })
                {
                    StatusCode = 500,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
