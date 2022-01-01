using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace showdoc_server.Attributes
{
    public class ShowdocAuthorizeAttribute : AuthorizeAttribute
    {
        public ShowdocAuthorizeAttribute()
        {
            this.Policy = "TokenValidator";
        }
    }
}
