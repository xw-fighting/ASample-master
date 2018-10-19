using Newtonsoft.Json;

namespace ASample.ThirdParty.WeChat.WeChatMessageSend.Model.InParam
{
    public class SendMsgParameter<T> : MsgTemplateBasicParameter where T : MsgTemplateDataBasicParameter
    {
        /// <summary>
        /// 消息参数
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
