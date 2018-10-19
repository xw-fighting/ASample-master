using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace ASample.ThirdParty.OAuth.QQ
{
    /// <summary>
    /// Provides context information to middleware providers.
    /// </summary>
    public class QQReturnEndpointContext : ReturnEndpointContext
    {
        public QQReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket) : base(context, ticket)
        {
        }
    }
}
