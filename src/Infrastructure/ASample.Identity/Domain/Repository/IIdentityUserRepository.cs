using ASample.EntityFramework.Domain.Repository;
using ASample.Identity.Domain.Models.AggregateRoots;
using System;

namespace ASample.Identity.Domain.Repository
{
    public interface IIdentityUserRepository : IBasicEntityFrameworkRepository<IdentityUser,Guid>
    {

    }
}
