using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxJsonInput
{
    /// <summary>
    /// 腾讯活体检测信息输入模型
    /// </summary>
    public class TxLiveCheckInfo
    {
        /// <summary>
        /// 活体检测凭据
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// appid
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty("sig")]
        public string Sign { get; set; }
    }
}
