using System.ComponentModel.DataAnnotations;

namespace ASample.WebSite.Core.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
