using Newtonsoft.Json;

namespace ASample.WebSite.Models.WeiboOAuth
{
    public class AccessTokenParam
    {
        [JsonProperty("client_id")]
        public string AppKey { get; set; }

        [JsonProperty("client_secret")]
        public string AppSecret { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUrl { get; set; }
    }
}