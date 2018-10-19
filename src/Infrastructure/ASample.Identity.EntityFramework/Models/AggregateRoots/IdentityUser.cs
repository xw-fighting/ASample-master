using ASample.Identity.EntityFramework.Models.Entities;
using ASmaple.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Identity.EntityFramework.Models.AggregateRoots
{
    public class IdentityUser : AggregateRoot, IUser<Guid>
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        //public IEnumerable<IdentityUserLoginInfo> LoginInfos { get; set; }

        public virtual ICollection<IdentityUserRole> Roles { get; }
        public virtual ICollection<IdentityUserClaim> Claims { get; }
        public virtual ICollection<IdentityUserLogin> Logins { get; }
    }
}
