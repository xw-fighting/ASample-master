using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Logging;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using ASample.ThirdParty.OAuth.QQ.Model;

namespace ASample.ThirdParty.OAuth.QQ
{
    public class QQAuthenticationHandler : AuthenticationHandler<QQAuthenticationOptions>
    {

        private const string AuthorizationEndpoint = "https://graph.qq.com/oauth2.0/authorize";
        private const string TokenEndpoint = "https://graph.qq.com/oauth2.0/token";
        private const string TokenRefreshEndpoint = "https://graph.qq.com/oauth2.0/token";
        private const string UserOpenIdEndpoint = "https://graph.qq.com/oauth2.0/me";
        private const string UserInfoEndpoint = "https://graph.qq.com/user/get_user_info";

        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public QQAuthenticationHandler(HttpClient httpClient,ILogger logger)
        {
            this._httpClient = httpClient;
            this._logger = logger;
        }
        

        /// <summary>
        /// 授权处理
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationProperties properties = null;
            try
            {
                var code = string.Empty;
                var state = string.Empty;

                var query = Request.Query;
                var values = query.GetValues("code");
                if(values != null && values.Count == 1)
                {
                    code = values[0];
                }
                values = query.GetValues("state");
                if(values != null && values.Count == 1)
                {
                    state = values[0];
                }

                properties = Options.StateDataFormat.Unprotect(state);
                if(properties == null)
                {
                    return null;
                }

                // OAuth2 10.12 CSRF
                if (!ValidateCorrelationId(properties, _logger))
                {
                    return new AuthenticationTicket(null, properties);
                }

                // 获取 accessToken 授权令牌
                var oauth2Token = await GetAccessTokenAsync(code);
                var accessToken = oauth2Token["access_token"];
                var refreshToken = oauth2Token["refresh+token"];
                var expire = oauth2Token["expire_in"];

                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    _logger.WriteWarning("Access Token was found");
                    return new AuthenticationTicket(null, properties);
                }

                //获取用户OpenId 
                var userOpenIdToken = await GetUserOpenIdAsync(accessToken);

                var userOpenId = userOpenIdToken["openId"].Value<string>();
                if (string.IsNullOrWhiteSpace(userOpenId))
                {
                    _logger.WriteWarning("User openId was not found");
                    return new AuthenticationTicket(null, properties);
                }

                //获取用户个人信息
                var userInfoToken = await GetUserInfoAsync(accessToken, userOpenId);

                var context = new QQAuthenticatedContext(Context, userOpenId, userInfoToken, accessToken, refreshToken, expire);
                context.Identity = new ClaimsIdentity
                    (
                        new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier,context.OpenId,"http://www.w3.org/2001/XMLSchema#string",Options.AuthenticationType),
                            new Claim(ClaimTypes.Name,context.NickName, "http://www.w3.org/2001/XMLSchema#string",Options.AuthenticationType),
                            new Claim("urn:qqconnect:id",context.OpenId, "http://www.w3.org/2001/XMLSchema#string", Options.AuthenticationType),
                            new Claim("urn:qqconnect:name",context.NickName, "http://www.w3.org/2001/XMLSchema#string", Options.AuthenticationType),
                            new Claim(Constants.QQClaimType,userInfoToken.ToString(),"http://www.w3.org/2001/XMLSchema#string", Options.AuthenticationType)
                        },Options.AuthenticationType,
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType
                     );
                context.Properties = properties;

                // 没有Email
                //if (!string.IsNullOrWhiteSpace(context.Email))
                //{
                //    context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Email, "http://www.w3.org/2001/XMLSchema#string", Options.AuthenticationType));
                //}    
                await Options.Provider.Authenticated(context);
                return new AuthenticationTicket(context.Identity, context.Properties);
            }
            catch (Exception ex)
            {
                _logger.WriteError("Authentication failed", ex);
                return new AuthenticationTicket(null, properties);
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userOpenId"></param>
        /// <returns></returns>
        private async Task<JObject> GetUserInfoAsync(string accessToken, string userOpenId)
        {
            var userInfoRequestUrl = string.Format("{0}?access_token={1}&oauth_consumer_key={2}&openid={3}", UserOpenIdEndpoint, accessToken, Options.AppId, userOpenId);
            var userInfoResponse = await _httpClient.GetAsync(userInfoRequestUrl, Request.CallCancelled);
            userInfoResponse.EnsureSuccessStatusCode();

            var userInfoResponseStr = await userInfoResponse.Content.ReadAsStringAsync();
            
            var userInfo = JObject.Parse(userInfoResponseStr);

            return userInfo;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 获取用户OpenId 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private async Task<JObject> GetUserOpenIdAsync(string accessToken)
        {
            var openIdRequestUrl = string.Format("{0}?access_token={1}", UserOpenIdEndpoint, accessToken);
            var openIdResponse = await _httpClient.GetAsync(openIdRequestUrl, Request.CallCancelled);
            var oauthTokenResponse = await openIdResponse.Content.ReadAsStringAsync();

            //callback  ( {"client_id":"YOUR_APPID","openid":"YOUR_OPENID"} );\n
            oauthTokenResponse = oauthTokenResponse.Remove(0, 9);
            oauthTokenResponse = oauthTokenResponse.Remove(oauthTokenResponse.Length - 3);

            JObject oauth2Token = JObject.Parse(oauthTokenResponse);
            return oauth2Token;

            //throw new NotImplementedException();
        }

        /// <summary>
        /// 获取 OauthToken 以键值对的方式存储
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private async Task<Dictionary<string, string>> GetAccessTokenAsync(string code)
        {
            var baseUri = Request.Scheme + Uri.SchemeDelimiter + Request.Host + Request.PathBase;
            var currentUri = baseUri + Request.Path + Request.QueryString;

            var redirectUri = baseUri + Options.CallbackPath;
            var requestParameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id",Options.AppId),
                new KeyValuePair<string, string>("client_secret",Options.AppSecret),
                new KeyValuePair<string, string>("code",code),
                new KeyValuePair<string, string>("grant_type","authorization_code"),
                new KeyValuePair<string, string>("redirect_uri",redirectUri)
            };
            var parameterBuilder = new StringBuilder();
            foreach (var item in requestParameters)
            {
                parameterBuilder.AppendFormat("{0}={1}&", Uri.EscapeDataString(item.Key), Uri.EscapeDataString(item.Value));
            }
            parameterBuilder.Length--;

            var parameterString = parameterBuilder.ToString();

            var requestTokenUrl = string.Format("{0}?{1}", TokenEndpoint, parameterString);
            var tokenResponse = await _httpClient.GetAsync(requestTokenUrl, Request.CallCancelled);

            var authToken = await tokenResponse.Content.ReadAsStringAsync();
            var oauthTokenDict = new Dictionary<string, string>();

            var responseParams = authToken.Split('&');
            foreach (var param in responseParams)
            {
                var kv = param.Split('=');
                oauthTokenDict[kv[0]] = kv[1];
            }
            return oauthTokenDict;
            //throw new NotImplementedException();
        }

        /// <summary>
        ///  执行401跳转
        /// </summary>
        /// <returns></returns>
        protected override Task ApplyResponseChallengeAsync()
        {
            //return base.ApplyResponseChallengeAsync();
            if(Response.StatusCode != 401)
            {
                return Task.FromResult<object>(null);
            }

            var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);

            if(challenge != null)
            {
                var baseUri = Request.Scheme + Uri.SchemeDelimiter + Request.Host + Request.QueryString;
                var currentUri = baseUri + Request.Path + Request.QueryString;
                var redirectUri = baseUri + Options.CallbackPath;

                var extra = challenge.Properties;
                if (string.IsNullOrEmpty(extra.RedirectUri))
                {
                    extra.RedirectUri = currentUri;
                }
                // OAuth2 10.12 CSRF
                GenerateCorrelationId(extra);

                var scope = string.Join(",", Options.Scope);
                if (string.IsNullOrEmpty(scope))
                {
                    scope = "get_user_info";
                }

                var state = Options.StateDataFormat.Protect(extra);
                var authenticationEndpoint = string.Format("{0}?client_id={1}&redirect_uri={2}$respone_type=code&scope={3}&state={4}",
                    AuthorizationEndpoint,
                    Uri.EscapeDataString(Options.AppId),
                    Uri.EscapeDataString(redirectUri),
                    Uri.EscapeDataString(scope),
                    Uri.EscapeDataString(state));
                var redirectContext = new QQApplyRedirectContext(Context, Options, extra, authenticationEndpoint);
                Options.Provider.ApplyRedirect(redirectContext);
            }
            return Task.FromResult<object>(null);

        }

        public override async Task<bool> InvokeAsync()
        {
            //return base.InvokeAsync();
            if (Options.CallbackPath.HasValue && Options.CallbackPath == Request.Path)
            {
                return await InvokeReturnPathAsync();
            }
            return false;
        }

        private async Task<bool> InvokeReturnPathAsync()
        {
            //throw new NotImplementedException();
            var model = await AuthenticateAsync();
            if(model == null)
            {
                _logger.WriteWarning("Invalid return state ,unable to redirect.");
                Response.StatusCode = 500;
                return true;
            }

            var context = new QQReturnEndpointContext(Context, model);
            context.SignInAsAuthenticationType = Options.SignInAsAuthenticationType;
            context.RedirectUri = model.Properties.RedirectUri;
            model.Properties.RedirectUri = null;

            await Options.Provider.ReturnEndpoint(context);

            if(context.SignInAsAuthenticationType != null && context.Identity != null)
            {
                var signInIdentity = context.Identity;
                if (!string.Equals(signInIdentity.AuthenticationType, context.SignInAsAuthenticationType, StringComparison.Ordinal))
                {
                    signInIdentity = new ClaimsIdentity(signInIdentity.Claims, context.SignInAsAuthenticationType, signInIdentity.NameClaimType, signInIdentity.RoleClaimType);
                }
                Context.Authentication.SignIn(context.Properties, signInIdentity);
            }

            if(!context.IsRequestCompleted && context.RedirectUri != null)
            {
                if(context.Identity == null)
                {
                    // add a redirect hint that sign-in failed in some way
                    context.RedirectUri = context.RedirectUri; //WebUtilities.AddQueryString(context.RedirectUri, "error", "access_denied");
                }
                Response.Redirect(context.RedirectUri);
                context.RequestCompleted();
            }
            return context.IsRequestCompleted;
        }

        private string GenerateRedirectUri()
        {
            string requestPrefix = Request.Scheme + "://" + Request.Host;

            string redirectUri = requestPrefix + RequestPathBase + Options.CallbackPath; // + "?state=" + Uri.EscapeDataString(Options.StateDataFormat.Protect(state));
            return redirectUri;
        }
    }
}
