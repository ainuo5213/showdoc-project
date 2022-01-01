using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Dtos.Enums;

namespace showdoc_server.Dtos.Json
{
    public class Result
    {
        public Errno Errno { get; set; } = Errno.Success;
        public string Errmsg { get; set; } = string.Empty;
        public object Data { get; set; } = null;
        public Result(object data)
        {
            this.Data = data;
        }
    }
}
