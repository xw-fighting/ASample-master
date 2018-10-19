using Owin;
using System;

namespace ASample.ThirdParty.OAuth.QQ
{
    /// <summary>
    /// Extension methods for using <see cref="QQAuthenticationMiddleware"/>
    /// </summary>
    public static class QQAuthenticationExtensions
    {
        /// <summary>
        /// Authenticate users using QQ
        /// </summary>
        public static IAppBuilder UserQQAuthentication(this IAppBuilder app,QQAuthenticationOptions options)
        {
            if(app == null)
            {
                throw new ArgumentNullException("app");
            }
            if(options == null)
            {
                throw new ArgumentNullException("options");
            }

            app.Use(typeof(QQAuthenticationMiddleware), app, options);
            return app;
        }

        /// <summary>
        /// Authenticate users using QQ
        /// </summary>
        public static IAppBuilder UserQQAuthentication(this IAppBuilder app,string appId,string appSecret)
        {
            return UserQQAuthentication(app, new QQAuthenticationOptions()
                { AppId = appId, AppSecret = appSecret });
        }
    }
}
