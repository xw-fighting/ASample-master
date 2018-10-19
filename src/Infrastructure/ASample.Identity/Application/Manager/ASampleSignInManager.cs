using ASample.Identity.Domain.Models.AggregateRoots;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASample.Identity.Application.Manager
{
    public  class ASampleSignInManager : SignInManager<IdentityUser,Guid>
    {
        public ASampleSignInManager(ASampleUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(IdentityUser user)
        {
            var userIdentity = await ((ASampleUserManager)UserManager).CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public static ASampleSignInManager Create(IdentityFactoryOptions<ASampleSignInManager> options, IOwinContext context)
        {
            return new ASampleSignInManager(context.GetUserManager<ASampleUserManager>(), context.Authentication);
        }
    }
}
