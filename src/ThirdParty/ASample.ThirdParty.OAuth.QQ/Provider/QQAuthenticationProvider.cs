using System;
using System.Threading.Tasks;

namespace ASample.ThirdParty.OAuth.QQ
{
    public class QQAuthenticationProvider : IQQAuthenticationProvider
    {
        /// <summary>
        /// Gets or sets the function that is invoked when the Authenticated method is invoked.
        /// 在Authenticated 方法被调用时设置和获取方法
        /// </summary>
        public Func<QQAuthenticatedContext, Task> OnAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets the function that is invoked when the ReturnEndpoint method is invoked.
        /// 在ReturnEndpoint 方法被调用时设置和获取方法
        /// </summary>
        public Func<QQReturnEndpointContext, Task> OnReturnEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the delegate that is invoked when the ApplyRedirect method is invoked.
        /// 在ApplyRedirect方法被调用时设置和获取该委托
        /// </summary>
        public Action<QQApplyRedirectContext> OnApplyRedirect { get; set; }

        public QQAuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
            OnReturnEndpoint = context => Task.FromResult<object>(null);
            OnApplyRedirect = context => context.Response.Redirect(context.RedirectUri);
        }

        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint
        /// </summary>
        public void ApplyRedirect(QQApplyRedirectContext context)
        {
            OnApplyRedirect(context);
        }

        /// <summary>
        /// Invoked whenever succesfully authenticates a user
        /// </summary>
        /// <param name="context"></param>
        public Task Authenticated(QQAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        /// <summary>
        /// Invoked prior to the <see cref="System.Security.Claims.ClaimsIdentity"/> being saved in a local cookie and the browser being redirected to the originally requested URL.
        /// </summary>
        /// <param name="context"></param>
        public Task ReturnEndpoint(QQReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
        }
    }
}
