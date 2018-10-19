using ASample.EntityFramework.Storage.Repository;
using ASample.Identity.Domain.Models.AggregateRoots;
using ASample.Identity.Domain.Repository;
using System;

namespace ASample.Identity.Storage.Repository
{
    public class UserRepository : BasicEntityFrameworkRepository<ASampleIdentityContext, User, Guid>, IUserRepository
    {
    }
}
