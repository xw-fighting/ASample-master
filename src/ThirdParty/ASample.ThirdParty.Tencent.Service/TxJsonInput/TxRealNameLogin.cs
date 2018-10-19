using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxJsonInput
{
    /// <summary>
    /// 腾讯实名登录输入模型
    /// </summary>
    public class TxRealNameLogin
    {

        /// <summary>
        /// 分配的appid
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 用户唯一标识，如：微信用户openid
        /// </summary>
        [JsonProperty("uid")]
        public string UserId { get; set; }

        /// <summary>
        /// 上述参数使用“-”拼接，拼接后的字符串，再最后拼接上SIG_KEY,然后字符串md5,取32位小写字符串(md5(参数1-参数2-...-私钥key))
        /// </summary>
        [JsonProperty("sig")]
        public string SignKey { get; set; }


    }
}
