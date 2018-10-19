using ASample.Identity.Sample.Core;
using ASample.Identity.Sample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASample.Identity.Sample
{
    /// <summary>
    /// To initialize the OWIN identity components we need to add a Startup class to the project with a method Configuration that takes an OWIN IAppBuilder instance as a parameter. This class will be automatically located and initialized by the OWIN host:
    /// </summary>
    public class Startup
    {
        public static Func<UserManager<AppUser>> UserManagerFactory { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //This is a string value that identifies the the cookie. This is necessary since we may have several instances of the Cookie middleware. For example, when using external auth servers (OAuth/OpenID) the same cookie middleware is used to pass claims from the external provider. If we'd pulled in the  Microsoft.AspNet.Identity NuGet package we could just use the constant  DefaultAuthenticationTypes.ApplicationCookie which has the same value - "ApplicationCookie".
                AuthenticationType = "ApplicationCookie",
                //The path to which the user agent (browser) should be redirected to when your application returns an unauthorized (401) response. This should correspond to your "login" controller. In this case I have an AuthContoller with a LogIn action
                LoginPath = new PathString("/auth/login")
            });

            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<AppUser>(
                    new UserStore<AppUser>(new AppDbContext()));
                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<AppUser>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                return usermanager;
            };
        }
    }
}