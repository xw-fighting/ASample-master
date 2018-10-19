using ASample.EntityFramework.Storage.QueryEntry;
using ASample.Identity.Domain.Models.AggregateRoots;
using ASample.Identity.Domain.QueryEntry;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.Identity.Storage.QueryEntry
{
    public class UserQueryEntry : BasicEntityFrameworkQueryEntry<ASampleIdentityContext, User, Guid>, IUserQueryEntry
    {
        /// <summary>
        /// 通过用户名获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<User> GetByNameAsync(string name)
        {
            var result = await SelectAsync(q => q.UserName == name);
            return result.FirstOrDefault();
        }



    }
}
