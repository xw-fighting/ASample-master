using Newtonsoft.Json;

namespace ASample.WebSite.Models.WeiboOAuth
{
    public class AccessTokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("remind_in")]
        public string RemindIn { get; set; }

        [JsonProperty("uid")]
        public string UId { get; set; }
    }
}