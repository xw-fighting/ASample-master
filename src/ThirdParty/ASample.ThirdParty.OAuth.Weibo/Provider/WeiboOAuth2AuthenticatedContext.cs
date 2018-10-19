using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.OAuth.Weibo.Provider
{
    public class WeiboOAuth2AuthenticatedContext:BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="WeiboOAuth2AuthenticatedContext"/>
        /// </summary>
        /// <param name="context">The OWIN environment</param>
        /// <param name="user">The JSON-serialized Weibo user info</param>
        /// <param name="accessToken">Weibo OAuth 2.0 access token</param>
        /// <param name="email">Weibo OAuth 2.0 refresh token</param>
        /// <param name="expires">Seconds until expiration</param>
        public WeiboOAuth2AuthenticatedContext(IOwinContext context, JObject user, string accessToken,
            string email)
            : base(context)
        {
            IDictionary<string, JToken> userAsDictionary = user;

            User = user;
            AccessToken = accessToken;

            Id = User["id"].ToString();
            Name = PropertyValueIfExists("screen_name", userAsDictionary);

            Email = email;
        }


        public JObject User { get; private set; }
        public string AccessToken { get; private set; }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public ClaimsIdentity Identity { get; set; }
        public AuthenticationProperties Properties { get; set; }

        private static string PropertyValueIfExists(string property, IDictionary<string, JToken> dictionary)
        {
            return dictionary.ContainsKey(property) ? dictionary[property].ToString() : null;
        }
    }
}
