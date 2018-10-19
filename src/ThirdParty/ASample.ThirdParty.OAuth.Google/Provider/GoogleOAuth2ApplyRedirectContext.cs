using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace ASample.ThirdParty.OAuth.Google.Provider
{
    /// <summary>
    /// Context passed when a Challenge causes a redirect to authorize endpoint in the Google OAuth 2.0 middleware
    /// </summary>
    public class GoogleOAuth2ApplyRedirectContext: BaseContext<GoogleOAuth2AuthenticationOptions>
    {
        public GoogleOAuth2ApplyRedirectContext(IOwinContext context, GoogleOAuth2AuthenticationOptions options,
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
