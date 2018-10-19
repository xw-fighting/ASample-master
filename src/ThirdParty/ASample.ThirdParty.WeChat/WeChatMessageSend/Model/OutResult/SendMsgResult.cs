using Newtonsoft.Json;

namespace ASample.ThirdParty.WeChat.WeChatMessageSend.Model.OutResult
{
    public class SendMsgResult
    {
        /// <summary>
        /// 返回错误码：0 表示成功
        /// </summary>
        [JsonProperty("errcode")]
        public string Errcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("errmsg")]
        public string Errmsg { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        [JsonProperty("messageid")]
        public string MessageId { get; set; }
    }
}
