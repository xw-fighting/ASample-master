using ASample.EntityFramework.Storage.QueryEntry;
using ASample.Identity.Domain.Models.AggregateRoots;
using ASample.Identity.Domain.Models.Entities;
using ASample.Identity.Domain.QueryEntry;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.Identity.Storage.QueryEntry
{
    public class IdentityUserQueryEntry : BasicEntityFrameworkQueryEntry<ASampleIdentityContext,IdentityUser,Guid>,IIdentityUserQueryEntry
    {
        protected DbSet<IdentityPassword> Set
        {
            get { return db.Set<IdentityPassword>(); }
        }

        protected IQueryable<IdentityPassword> DbSet
        {
            get { return Set.AsNoTracking(); }
        }

        public async Task<IdentityUser> GetByIdAsync(Guid id)
        {
            var result = await SelectAsync(q => q.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IdentityUser> GetByNameAsync(string userName)
        {
            var result = await SelectAsync(q => q.UserName == userName);
            return result.FirstOrDefault();
        }

        public  async Task<IdentityPassword> GetPasswordHashByUserIdAsync(Guid userId)
        {
            var result = await DbSet.FirstOrDefaultAsync(c => c.Id == userId);
            return result;
        }
    }
}
