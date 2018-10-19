using ASample.EntityFramework.Domain.QueryEntry;
using ASample.EntityFramework.Storage.QueryEntry;
using ASample.Identity.EntityFramework;
using ASample.Identity.EntityFramework.Models.AggregateRoots;
using System;
using System.Threading.Tasks;

namespace ASample.Identity.EntityFramework.Domain.QueryEntry
{
    public class IdentityUserQueryEntry : BasicEntityFrameworkQueryEntry<ASampleIdentityDbContext, IdentityUser,Guid>,IIdentityUserQueryEntry
    {

        /// <summary>
        /// 通过用户编号获取密码密文
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //Task<IdentityPassword> GetPasswordHashByUserIdAsync(Guid userId);
    }
}
