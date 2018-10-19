using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxJsonInput
{
    /// <summary>
    /// 活体验证码输入模型
    /// </summary>
    public class TxLiveVerificationCode
    {
        /// <summary>
        /// 分配的appid
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// auth接口返回的token
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// 参数签名校验(md5(参数1-参数2-...-私钥key))
        /// </summary>
        [JsonProperty("sig")]
        public string Sign { get; set; }
    }
}
