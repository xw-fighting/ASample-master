using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace ASample.ThirdParty.OAuth.Weibo.Provider
{
    public class WeiboOAuth2ApplyRedirectContext : BaseContext<WeiboOAuth2AuthenticationOptions>
    {
        public WeiboOAuth2ApplyRedirectContext(IOwinContext context, WeiboOAuth2AuthenticationOptions options,
           AuthenticationProperties properties, string redirectUri)
           : base(context, options)
        {
            RedirectUri = redirectUri;
            Properties = properties;
        }

        /// <summary>
        /// Gets the URI used for the redirect operation.
        /// </summary>
        public string RedirectUri { get; private set; }

        /// <summary>
        /// Gets the authenticaiton properties of the challenge
        /// </summary>
        public AuthenticationProperties Properties { get; private set; }
    }
}
