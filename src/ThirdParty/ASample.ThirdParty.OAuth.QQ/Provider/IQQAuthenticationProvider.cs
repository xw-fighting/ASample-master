using System.Threading.Tasks;

namespace ASample.ThirdParty.OAuth.QQ
{
    /// <summary>
    /// Specifies callback methods which the <see cref="QQAuthenticationMiddleware"></see> invokes to enable developer control over the authentication process. />
    /// </summary>
    public interface IQQAuthenticationProvider
    {
        Task Authenticated(QQAuthenticatedContext context);

        Task ReturnEndpoint(QQReturnEndpointContext context);

        void ApplyRedirect(QQApplyRedirectContext context);
    }
}
