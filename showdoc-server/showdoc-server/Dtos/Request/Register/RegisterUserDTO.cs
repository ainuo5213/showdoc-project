using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Attributes;

namespace showdoc_server.Dtos.Request.Register
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "username needed")]
        [MinLength(1, ErrorMessage = "username must 1 char at least")]
        [MaxLength(18, ErrorMessage = "username must 18 chars at most")]
        public string Username { get; set; }
        [Required(ErrorMessage = "cellphone needed")]
        [Cellphone(ErrorMessage = "not a phone number")]
        public string Cellphone { get; set; }
        [Required(ErrorMessage = "password needed")]
        public string Password { get; set; }
        [Required(ErrorMessage = "verify code needed")]
        public string VerifyCode { get; set; }
    }
}
