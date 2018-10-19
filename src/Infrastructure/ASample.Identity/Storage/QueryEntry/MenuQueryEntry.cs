using ASample.EntityFramework.Storage.QueryEntry;
using ASample.Identity.Domain.Models.AggregateRoots;
using ASample.Identity.Domain.QueryEntry;
using System;

namespace ASample.Identity.Storage.QueryEntry
{
    public class MenuQueryEntry : BasicEntityFrameworkQueryEntry<ASampleIdentityContext, Menu, Guid>, IMenuQueryEntry
    {
    }
}
