using ASample.EntityFramework.Storage.Repository;
using ASample.Identity.Domain.Models.AggregateRoots;
using ASample.Identity.Domain.Repository;
using System;

namespace ASample.Identity.Storage.Repository
{
    public class MenuRepository : BasicEntityFrameworkRepository<ASampleIdentityContext, Menu, Guid>, IMenuRepository
    {
    }
}
