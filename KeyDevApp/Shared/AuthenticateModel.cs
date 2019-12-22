using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyDevApp.Shared
{
    public class AuthenticateModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
