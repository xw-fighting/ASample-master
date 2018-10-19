using ASample.ThirdParty.OAuth.Weibo.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.OAuth.Weibo
{
    public class WeiboOAuth2AuthenticationHandler : AuthenticationHandler<WeiboOAuth2AuthenticationOptions>
    {
        private const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        public WeiboOAuth2AuthenticationHandler(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationProperties properties = null;

            try
            {
                string code = null;
                string state = null;

                IReadableStringCollection query = Request.Query;
                IList<string> values = query.GetValues("code");
                if (values != null && values.Count == 1)
                {
                    code = values[0];
                }
                values = query.GetValues("state");
                if (values != null && values.Count == 1)
                {
                    state = values[0];
                }

                properties = Options.StateDataFormat.Unprotect(state);
                if (properties == null)
                {
                    return null;
                }

                // OAuth2 10.12 CSRF
                if (!ValidateCorrelationId(Options.CookieManager, properties, _logger))
                {
                    return new AuthenticationTicket(null, properties);
                }

                string requestPrefix = Request.Scheme + "://" + Request.Host;
                string redirectUri = requestPrefix + Request.PathBase + Options.CallbackPath;

                // Build up the body for the token request
                var tokenRequestParameters = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri),
                    new KeyValuePair<string, string>("client_id", Options.ClientId),
                    new KeyValuePair<string, string>("client_secret", Options.ClientSecret)
                };

                // Request the token
                var content = new FormUrlEncodedContent(tokenRequestParameters);
                HttpResponseMessage tokenResponse = await _httpClient.PostAsync(Options.TokenEndpoint, content);
                tokenResponse.EnsureSuccessStatusCode();
                string responseString = await tokenResponse.Content.ReadAsStringAsync();

                // Deserializes the token response
                JObject response = JObject.Parse(responseString);
                string accessToken = response.Value<string>("access_token");

                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    _logger.WriteWarning("Access token was not found");
                    return new AuthenticationTicket(null, properties);
                }

                // Get the weibo user
                var userInfoUrl = Options.UserInformationEndpoint + "?access_token=" + Uri.EscapeDataString(accessToken) + "&uid=" + response["uid"].Value<string>();
                var userResponse =await _httpClient.GetAsync(userInfoUrl, Request.CallCancelled);

                var responseStr = await userResponse.Content.ReadAsStringAsync();
                JObject accountInfo = JObject.Parse(responseStr);

                string email = String.Empty;
                if (Options.RequireEmail)
                {
                    HttpResponseMessage emailResponse = await _httpClient.GetAsync(
                        Options.UserEmailDetailEndpoint + "?access_token=" + Uri.EscapeDataString(accessToken),
                        Request.CallCancelled);
                    emailResponse.EnsureSuccessStatusCode();
                    string emailString = await emailResponse.Content.ReadAsStringAsync();
                    email = JObject.Parse(responseStr)["email"].Value<string>();
                }

                var context = new WeiboOAuth2AuthenticatedContext(Context, accountInfo, accessToken,email);
                context.Identity = new ClaimsIdentity(new[]{
                    new Claim(ClaimTypes.NameIdentifier, context.Id,XmlSchemaString,Options.AuthenticationType),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, context.Name,XmlSchemaString,Options.AuthenticationType),
                    new Claim("urn:sinaweibo:id", context.Id,XmlSchemaString,Options.AuthenticationType),
                    new Claim("urn:sinaweibo:name", context.Name,XmlSchemaString,Options.AuthenticationType),
                });

                if (!string.IsNullOrWhiteSpace(context.Email))
                {
                    context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Email, XmlSchemaString, Options.AuthenticationType));
                }

                await Options.Provider.Authenticated(context);

                context.Properties = properties;

                return new AuthenticationTicket(context.Identity, context.Properties);

                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //HttpResponseMessage graphResponse = await _httpClient.SendAsync(request, Request.CallCancelled);
                //graphResponse.EnsureSuccessStatusCode();
                //text = await graphResponse.Content.ReadAsStringAsync();
                //JObject user = JObject.Parse(text);

                //var context = new WeiboOAuth2AuthenticatedContext(Context, user, response);
                //context.Identity = new ClaimsIdentity(
                //    Options.AuthenticationType,
                //    ClaimsIdentity.DefaultNameClaimType,
                //    ClaimsIdentity.DefaultRoleClaimType);
                //if (!string.IsNullOrEmpty(context.Id))
                //{
                //    context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.Id,
                //        ClaimValueTypes.String, Options.AuthenticationType));
                //}
                //if (!string.IsNullOrEmpty(context.GivenName))
                //{
                //    context.Identity.AddClaim(new Claim(ClaimTypes.GivenName, context.GivenName,
                //        ClaimValueTypes.String, Options.AuthenticationType));
                //}
                //if (!string.IsNullOrEmpty(context.FamilyName))
                //{
                //    context.Identity.AddClaim(new Claim(ClaimTypes.Surname, context.FamilyName,
                //        ClaimValueTypes.String, Options.AuthenticationType));
                //}
                //if (!string.IsNullOrEmpty(context.Name))
                //{
                //    context.Identity.AddClaim(new Claim(ClaimTypes.Name, context.Name, ClaimValueTypes.String,
                //        Options.AuthenticationType));
                //}
                //if (!string.IsNullOrEmpty(context.Email))
                //{
                //    context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Email, ClaimValueTypes.String,
                //        Options.AuthenticationType));
                //}

                //if (!string.IsNullOrEmpty(context.Profile))
                //{
                //    context.Identity.AddClaim(new Claim("urn:weibo:profile", context.Profile, ClaimValueTypes.String,
                //        Options.AuthenticationType));
                //}
                //context.Properties = properties;

                //await Options.Provider.Authenticated(context);

                //return new AuthenticationTicket(context.Identity, context.Properties);
            }
            catch (Exception ex)
            {
                _logger.WriteError("Authentication failed", ex);
                return new AuthenticationTicket(null, properties);
            }
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401)
            {
                return Task.FromResult<object>(null);
            }

            AuthenticationResponseChallenge challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);

            if (challenge != null)
            {
                string baseUri =Request.Scheme +Uri.SchemeDelimiter + Request.Host + Request.PathBase;

                string currentUri =  baseUri + Request.Path + Request.QueryString;

                string redirectUri = baseUri + Options.CallbackPath;

                AuthenticationProperties properties = challenge.Properties;
                if (string.IsNullOrEmpty(properties.RedirectUri))
                {
                    properties.RedirectUri = currentUri;
                }

                // OAuth2 10.12 CSRF
                GenerateCorrelationId(Options.CookieManager, properties);

                var queryStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                queryStrings.Add("response_type", "code");
                queryStrings.Add("client_id", Options.ClientId);
                queryStrings.Add("redirect_uri", redirectUri);

                string scope = string.Join(" ", Options.Scope);
                if (string.IsNullOrEmpty(scope))
                {
                    // weibo OAuth 2.0 asks for non-empty scope. If user didn't set it, set default scope to 
                    // "openid profile email" to get basic user information.
                    scope = "openid profile email";
                }
                string state = Options.StateDataFormat.Protect(properties);

                string authorizationEndpoint =
                    "https://api.weibo.com/oauth2/authorize" +
                        "?client_id=" + Uri.EscapeDataString(Options.ClientId ?? string.Empty) +
                        "&redirect_uri=" + Uri.EscapeDataString(redirectUri) +
                        "&scope=" + Uri.EscapeDataString(scope) +
                        "&state=" + Uri.EscapeDataString(state);

                //Response.Redirect(authorizationEndpoint);

                // space separated
                //string scope = string.Join(" ", Options.Scope);
                //if (string.IsNullOrEmpty(scope))
                //{
                //    // Google OAuth 2.0 asks for non-empty scope. If user didn't set it, set default scope to 
                //    // "openid profile email" to get basic user information.
                //    scope = "openid profile email";
                //}
                //AddQueryString(queryStrings, properties, "scope", scope);

                //AddQueryString(queryStrings, properties, "access_type", Options.AccessType);
                //AddQueryString(queryStrings, properties, "approval_prompt");
                //AddQueryString(queryStrings, properties, "prompt");
                //AddQueryString(queryStrings, properties, "login_hint");
                //AddQueryString(queryStrings, properties, "include_granted_scopes");

                //string state = Options.StateDataFormat.Protect(properties);
                //queryStrings.Add("state", state);

                //string authorizationEndpoint = WebUtilities.AddQueryString(Options.AuthorizationEndpoint,
                //    queryStrings);

                var redirectContext = new WeiboOAuth2ApplyRedirectContext(
                    Context, Options,
                    properties, authorizationEndpoint);
                Options.Provider.ApplyRedirect(redirectContext);
            }

            return Task.FromResult<object>(null);
        }

        public override async Task<bool> InvokeAsync()
        {
            return await InvokeReplyPathAsync();
        }

        private async Task<bool> InvokeReplyPathAsync()
        {
            if (Options.CallbackPath.HasValue && Options.CallbackPath == Request.Path)
            {
                // TODO: error responses

                AuthenticationTicket ticket = await AuthenticateAsync();
                if (ticket == null)
                {
                    _logger.WriteWarning("Invalid return state, unable to redirect.");
                    Response.StatusCode = 500;
                    return true;
                }

                var context = new WeiboOAuth2ReturnEndpointContext(Context, ticket);
                context.SignInAsAuthenticationType = Options.SignInAsAuthenticationType;
                context.RedirectUri = ticket.Properties.RedirectUri;

                await Options.Provider.ReturnEndpoint(context);

                if (context.SignInAsAuthenticationType != null &&
                    context.Identity != null)
                {
                    ClaimsIdentity grantIdentity = context.Identity;
                    if (!string.Equals(grantIdentity.AuthenticationType, context.SignInAsAuthenticationType, StringComparison.Ordinal))
                    {
                        grantIdentity = new ClaimsIdentity(grantIdentity.Claims, context.SignInAsAuthenticationType, grantIdentity.NameClaimType, grantIdentity.RoleClaimType);
                    }
                    Context.Authentication.SignIn(context.Properties, grantIdentity);
                }

                if (!context.IsRequestCompleted && context.RedirectUri != null)
                {
                    string redirectUri = context.RedirectUri;
                    if (context.Identity == null)
                    {
                        // add a redirect hint that sign-in failed in some way
                        redirectUri = WebUtilities.AddQueryString(redirectUri, "error", "access_denied");
                    }
                    Response.Redirect(redirectUri);
                    context.RequestCompleted();
                }

                return context.IsRequestCompleted;
            }
            return false;
        }

        private static void AddQueryString(IDictionary<string, string> queryStrings, AuthenticationProperties properties,
            string name, string defaultValue = null)
        {
            string value;
            if (!properties.Dictionary.TryGetValue(name, out value))
            {
                value = defaultValue;
            }
            else
            {
                // Remove the parameter from AuthenticationProperties so it won't be serialized to state parameter
                properties.Dictionary.Remove(name);
            }

            if (value == null)
            {
                return;
            }

            queryStrings[name] = value;
        }
    }
}
