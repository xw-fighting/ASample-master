using ASample.EntityFramework.Domain.Repository;
using ASample.Identity.Domain.Models.AggregateRoots;
using System;

namespace ASample.Identity.Domain.Repository
{
    public interface IRoleRepository : IBasicEntityFrameworkRepository<Role, Guid>
    {
    }
}
