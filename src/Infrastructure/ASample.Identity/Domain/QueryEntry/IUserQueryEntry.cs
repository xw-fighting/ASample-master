using ASample.EntityFramework.Domain.QueryEntry;
using ASample.Identity.Domain.Models.AggregateRoots;
using System;
namespace ASample.Identity.Domain.QueryEntry
{
    public interface IUserQueryEntry : IBasicEntityFrameworkQueryEntry<User, Guid>
    {
    }
}
