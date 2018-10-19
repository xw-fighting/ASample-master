using ASample.Identity.Domain.Models.AggregateRoots;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;


namespace ASample.Identity.Application.Manager
{
    public class ASampleUserManager : UserManager<IdentityUser, Guid>
    {
        public ASampleUserManager(ASampleUserStore store)
            : base(store)
        {

        }

        public static ASampleUserManager Create(IdentityFactoryOptions<ASampleUserManager> options,
          IOwinContext context)
        {
            var manager = new ASampleUserManager(new ASampleUserStore());
            // Configure validation logic for usernames
            //manager.UserValidator = new UserValidator<IdentityUser,Guid>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};
            // Configure validation logic for passwords
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 10,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};
            // Configure user lockout defaults
            //manager.UserLockoutEnabledByDefault = true;
            //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<IdentityUser, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
