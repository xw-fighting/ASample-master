using Newtonsoft.Json;

namespace ASample.WebSite.Models.WeCaht
{
    public class AccessOAuthTokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("openid")]
        public string Openid { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}