using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Identity.EntityFramework.Models.Entities
{
    public class IdentityUserLoginInfo
    {
        public Guid UserId { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
