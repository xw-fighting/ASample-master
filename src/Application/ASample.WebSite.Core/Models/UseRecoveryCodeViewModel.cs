using System.ComponentModel.DataAnnotations;

namespace ASample.WebSite.Core.Models
{
    public class UseRecoveryCodeViewModel
    {
        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }
    }
}
