using ASample.Identity.Domain.Models.AggregateRoots;
using ASample.Identity.Domain.QueryEntry;
using ASample.Identity.Domain.Repository;
using ASample.Identity.Storage.QueryEntry;
using ASample.Identity.Storage.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ASample.Identity.Domain.Models.Entities;
using System.Security.Claims;

namespace ASample.Identity.Application
{
    public class ASampleUserStore : IUserStore<IdentityUser, Guid>, 
                                    IUserLoginStore<IdentityUser, Guid>,
                                    IUserPasswordStore<IdentityUser, Guid>,
                                    IUserClaimStore<IdentityUser, Guid>,
                                    IUserEmailStore<IdentityUser,Guid>
    {

        public ASampleUserStore()
        {
            IdentityUserQueryEntry = new IdentityUserQueryEntry();
            IdentityUserRepository = new IdentityUserRepository();
            IdentityPasswordRepository = new IdentityPasswordRepository();
        }

        public IIdentityUserQueryEntry IdentityUserQueryEntry { get; set; }

        public IIdentityUserRepository IdentityUserRepository { get; set; }

        public IIdentityPasswordRepository IdentityPasswordRepository { get; set; }

        #region IUserLoginStore
        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
        #endregion



        #region IUserPasswordStore  

        public async Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            var result = await IdentityUserQueryEntry.GetPasswordHashByUserIdAsync(user.Id);
            return result.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(IdentityUser user)
        {
            var result = await IdentityUserQueryEntry.GetPasswordHashByUserIdAsync(user.Id);
            return !string.IsNullOrEmpty(result.PasswordHash);
        }


        public async Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            var model = new IdentityPassword
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                PasswordHash = passwordHash,

            };
            await IdentityPasswordRepository.AddAsync(model);
        }
        #endregion



        #region UserStore
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateAsync(IdentityUser user)
        {
            await IdentityUserRepository.AddAsync(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task DeleteAsync(IdentityUser user)
        {
            await IdentityUserRepository.DeleteAsync(user.Id);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }



        public async Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            var result = await IdentityUserQueryEntry.GetAsync(userId);
            return result;
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            var result = await IdentityUserQueryEntry.SelectAsync(c => c.UserName == userName);
            return result.ToList().FirstOrDefault();
        }



        public async Task UpdateAsync(IdentityUser user)
        {
            await IdentityUserRepository.UpdateAsync(user);
        }


        #endregion


        #region  IUserClaimStore
        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetEmailAsync(IdentityUser user)
        {
            return user.UserName;
            //throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
