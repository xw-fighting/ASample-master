using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using System;
using System.Net.Http;

namespace ASample.ThirdParty.OAuth.QQ
{
    public class QQAuthenticationMiddleware : AuthenticationMiddleware<QQAuthenticationOptions>
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        public QQAuthenticationMiddleware(OwinMiddleware next,IAppBuilder app,QQAuthenticationOptions options):base(next,options)
        {
            if (string.IsNullOrWhiteSpace(options.AppId))
            {
                throw new ArgumentException("AppID must be provider ,AppId 不能为空");
            }
            if(string.IsNullOrWhiteSpace(options.AppSecret))
            {
                throw new ArgumentException("AppSecret must be provider,AppSecret 不能为空");
            }

            _logger = app.CreateLogger<QQAuthenticationMiddleware>();

            if(Options.Provider == null)
            {
                Options.Provider = new QQAuthenticationProvider();
            }

            if(Options.StateDataFormat == null)
            {
                var dataProtecter = app.CreateDataProtector(typeof(QQAuthenticationMiddleware).FullName, Options.AuthenticationType, "v1");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtecter);
            }

            if (string.IsNullOrWhiteSpace(Options.SignInAsAuthenticationType))
            {
                Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();
            }

            _httpClient = new HttpClient(ResolveHttpMessageHandler(Options));
            _httpClient.Timeout = Options.BackchannelTimeout;
            _httpClient.MaxResponseContentBufferSize = 1024 * 1024 * 10;//10M

        }

        private HttpMessageHandler ResolveHttpMessageHandler(QQAuthenticationOptions options)
        {
            var handler = options.BackchannelHttpHandler ?? new WebRequestHandler();
            
            // If they provided a validator, apply it or fail.
            if (options.BackchannelCertificateValidator != null)
            {
                // Set the cert validate callback
                var webRequestHandler = handler as WebRequestHandler;

                if (webRequestHandler == null)
                {
                    throw new InvalidOperationException();
                }

                webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;
            }

            return handler;
        }

        protected override AuthenticationHandler<QQAuthenticationOptions> CreateHandler()
        {
            throw new NotImplementedException();
        }
    }
}
