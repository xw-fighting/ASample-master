using ASample.Identity.EntityFramework.Models.AggregateRoots;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.Owin;

namespace ASample.Web.Identity.Manager
{
    public class ASampleSignInManager : SignInManager<IdentityUser, Guid>
    {
        public ASampleSignInManager(ASampleUserManager userManager, IAuthenticationManager authenticationManager) 
            : base(userManager, authenticationManager)
        {
        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(IdentityUser user)
        {
            var userIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public static ASampleSignInManager Create(IdentityFactoryOptions<ASampleSignInManager> options, IOwinContext context)
        {
            return new ASampleSignInManager(context.GetUserManager<ASampleUserManager>(), context.Authentication);
        }
    }
}
