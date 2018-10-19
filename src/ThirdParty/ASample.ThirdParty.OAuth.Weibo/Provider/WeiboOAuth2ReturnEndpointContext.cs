using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace ASample.ThirdParty.OAuth.Weibo.Provider
{
    public class WeiboOAuth2ReturnEndpointContext : ReturnEndpointContext
    {
        /// <summary>
        /// Initialize a <see cref="WeiboOAuth2ReturnEndpointContext"/>
        /// </summary>
        /// <param name="context">OWIN environment</param>
        /// <param name="ticket">The authentication ticket</param>
        public WeiboOAuth2ReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket) : base(context, ticket)
        {

        }
    }
}
