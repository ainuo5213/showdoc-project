using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Services.Http
{
    public interface IHttpService
    {
        string Get(string url, Dictionary<string, string> parameters = null);
    }
}
