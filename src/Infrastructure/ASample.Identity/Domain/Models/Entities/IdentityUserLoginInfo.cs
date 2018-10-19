using ASmaple.Domain.Models;
using System;

namespace ASample.Identity.Domain.Models.Entities
{
    public class IdentityUserLoginInfo : Entity
    {
        public Guid UserId { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
