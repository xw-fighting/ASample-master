using ASample.EntityFramework.Domain.QueryEntry;
using ASample.Identity.Domain.Models.AggregateRoots;
using ASample.Identity.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace ASample.Identity.Domain.QueryEntry
{
    public interface IIdentityUserQueryEntry : IBasicEntityFrameworkQueryEntry<IdentityUser,Guid>
    {

        /// <summary>
        /// 通过用户编号获取密码密文
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IdentityPassword> GetPasswordHashByUserIdAsync(Guid userId);

        Task<IdentityUser> GetByNameAsync(string userName);

        Task<IdentityUser> GetByIdAsync(Guid id);

    }
}
