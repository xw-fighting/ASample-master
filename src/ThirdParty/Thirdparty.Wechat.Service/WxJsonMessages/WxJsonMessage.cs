using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 加工后的基类
    /// </summary>
    internal class WxJsonMessage
    {
        [JsonProperty("msgtype")]
        public string MessageType { get; protected set; }

        /// <summary>
        /// 原创校验，0不校验，1校验
        /// </summary>
        [JsonProperty("send_ignore_reprint")]
        public int Reprint { get; set; }
    }
}