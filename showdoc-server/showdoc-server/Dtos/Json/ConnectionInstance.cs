using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace showdoc_server.Dtos.Json
{
    public class ConnectionInstance
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
        public string Database { get; set; }

        public override string ToString()
        {
            return $"server={this.IP};user id={UserId};password={this.Password};database={this.Database}";
        }
    }
}
