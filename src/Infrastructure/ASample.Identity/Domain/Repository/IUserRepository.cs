using ASample.EntityFramework.Domain.Repository;
using ASample.Identity.Domain.Models.AggregateRoots;
using System;

namespace ASample.Identity.Domain.Repository
{
    public interface IUserRepository : IBasicEntityFrameworkRepository<User, Guid>
    {
    }
}
