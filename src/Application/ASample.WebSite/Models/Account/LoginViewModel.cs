using System;
using System.ComponentModel.DataAnnotations;

namespace ASample.WebSite.Models
{
    public class LoginViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="登录名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}