﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using showdoc_server.Attributes;

namespace showdoc_server.Dtos.Request.Auth
{
    public class PassForgetDTO
    {
        [Required(ErrorMessage = "cellphone needed")]
        [Cellphone(ErrorMessage = "not a phone number")]
        public string Cellphone { get; set; }
        [Required(ErrorMessage = "password needed")]
        [MinLength(6, ErrorMessage = "password must 6 char at least")]
        [MaxLength(18, ErrorMessage = "password must 6 char at most")]
        public string Password { get; set; }
        [Required(ErrorMessage = "verify code needed")]
        public string VerifyCode { get; set; }
    }
}
