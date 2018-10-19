using Microsoft.Owin.Security.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace ASample.ThirdParty.OAuth.QQ
{
    public class QQApplyRedirectContext : BaseContext<QQAuthenticationOptions>
    {
        /// <summary>
        ///  Context passed when a Challenge causes a redirect to authorize endpoint in the middleware
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        ///  Gets the authenticaiton properties of the challenge
        /// </summary>
        public AuthenticationProperties Properties { get; set; }
        public QQApplyRedirectContext(IOwinContext context, QQAuthenticationOptions options, AuthenticationProperties properties, string redirectUri) : base(context, options)
        {
            this.RedirectUri = redirectUri;
            this.Properties = Properties;
        }
    }
}
