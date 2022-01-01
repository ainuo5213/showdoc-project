using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using showdoc_server.Context;
using showdoc_server.Dtos.Table;

namespace showdoc_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("")]
        public IEnumerable<Users> Index()
        {
            return SugarContext.Context.Queryable<Users>().ToList();
        }
    }
}
