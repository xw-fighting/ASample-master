using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;


namespace ASample.ThirdParty.OAuth.Google.Provider
{
    /// <summary>
    /// Provides context information to middleware providers.
    /// </summary>
    public class GoogleOAuth2ReturnEndpointContext : ReturnEndpointContext
    {
        /// <summary>
        /// Initialize a <see cref="GoogleOAuth2ReturnEndpointContext"/>
        /// </summary>
        /// <param name="context">OWIN environment</param>
        /// <param name="ticket">The authentication ticket</param>
        public GoogleOAuth2ReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket) : base(context, ticket)
        {

        }
    }
}
