using Owin;
using System;

namespace ASample.ThirdParty.OAuth.Weibo
{
    public static class WeiboAuthenticationExtensions
    {
        /// <summary>
        /// Authenticate users using Weibo OAuth 2.0
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/> passed to the configuration method</param>
        /// <param name="options">Middleware configuration options</param>
        /// <returns>The updated <see cref="IAppBuilder"/></returns>
        public static IAppBuilder UseWeiboAuthentication(this IAppBuilder app, WeiboOAuth2AuthenticationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            app.Use(typeof(WeiboOAuth2AuthenticationMiddleware), app, options);
            return app;
        }

        /// <summary>
        /// Authenticate users using Weibo OAuth 2.0
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/> passed to the configuration method</param>
        /// <param name="clientId">The Weibo assigned client id</param>
        /// <param name="clientSecret">The Weibo assigned client secret</param>
        /// <returns>The updated <see cref="IAppBuilder"/></returns>
        public static IAppBuilder UseWeiboAuthentication(this IAppBuilder app,string clientId,string clientSecret)
        {
            return UseWeiboAuthentication(app,
                new WeiboOAuth2AuthenticationOptions
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                });
        }
    }
}
