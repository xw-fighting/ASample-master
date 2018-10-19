﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using ASample.Identity;
using ASample.Identity.Application.Manager;
using ASample.Identity.Domain.Models.AggregateRoots;

namespace ASample.WebSite
{
    public partial class Startup
    {
        // 有关配置身份验证的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // 配置数据库上下文、用户管理器和登录管理器，以便为每个请求使用单个实例
            app.CreatePerOwinContext(ASampleIdentityContext.Create);
            app.CreatePerOwinContext<ASampleUserManager>(ASampleUserManager.Create);
            app.CreatePerOwinContext<ASampleSignInManager>(ASampleSignInManager.Create);

            // 使应用程序可以使用 Cookie 来存储已登录用户的信息
            // 并使用 Cookie 来临时存储有关使用第三方登录提供程序登录的用户的信息
            // 配置登录 Cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // 当用户登录时使应用程序可以验证安全戳。
                    // 这是一项安全功能，当你更改密码或者向帐户添加外部登录名时，将使用此功能。
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ASampleUserManager, IdentityUser, Guid>(
                        TimeSpan.FromMinutes(30),
                        async (manager, user) =>
                        {
                            var identity = await manager.CreateIdentityAsync(user,
                                DefaultAuthenticationTypes.ApplicationCookie);
                            return identity;
                        },
                        identity => Guid.Parse(identity.GetUserId())
                        )
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 使应用程序可以在双重身份验证过程中验证第二因素时暂时存储用户信息。
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // 使应用程序可以记住第二登录验证因素，例如电话或电子邮件。
            // 选中此选项后，登录过程中执行的第二个验证步骤将保存到你登录时所在的设备上。
            // 此选项类似于在登录时提供的“记住我”选项。
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // 取消注释以下行可允许使用第三方登录提供程序登录
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

            //QQ第三方授权登录
            //app.UserQQAuthentication("1103988655", "VfQKhz9gXIZfEs7A");
            //app.UseWeiboAuthentication(new WeiboOAuth2AuthenticationOptions
            //{
            //    ClientId = "1588412454",
            //    ClientSecret = "259a80d6872f0697f3270a49f698e9fe"
            //});
        }

    }
}