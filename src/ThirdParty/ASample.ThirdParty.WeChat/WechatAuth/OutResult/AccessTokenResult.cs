using Newtonsoft.Json;

namespace ASample.ThirdParty.WeChat.WechatAuth.OutResult
{
    public class AccessTokenResult
    {

        /// <summary>
        /// 微信access_tokwn
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// access_token 过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpireTime { get; set; }
    }
}
