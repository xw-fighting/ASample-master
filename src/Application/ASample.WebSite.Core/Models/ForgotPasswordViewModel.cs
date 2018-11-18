using System.ComponentModel.DataAnnotations;

namespace ASample.WebSite.Core.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
