using ASample.EntityFramework.Domain.QueryEntry;
using ASample.Identity.EntityFramework.Models.AggregateRoots;
using System;
using System.Threading.Tasks;

namespace ASample.Identity.EntityFramework.Domain.QueryEntry
{
    public interface IIdentityUserQueryEntry : IBasicEntityFrameworkQueryEntry<IdentityUser,Guid>
    {

        /// <summary>
        /// 通过用户编号获取密码密文
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //Task<IdentityPassword> GetPasswordHashByUserIdAsync(Guid userId);
    }
}
