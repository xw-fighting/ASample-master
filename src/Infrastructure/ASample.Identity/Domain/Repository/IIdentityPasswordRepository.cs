using ASample.EntityFramework.Domain.Repository;
using ASample.Identity.Domain.Models.Entities;
using System;

namespace ASample.Identity.Domain.Repository
{
    public interface IIdentityPasswordRepository : IBasicEntityFrameworkRepository<IdentityPassword, Guid>
    {

    }
}
