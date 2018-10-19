using Newtonsoft.Json;

namespace ASample.ThirdParty.WeChat.WeChatMessageSend.Model.InParam
{
    public class MsgTemplateBasicParameter
    {
        /// <summary>
        /// 接收者openid
        /// </summary>
        [JsonProperty("touser")]
        public string ToUser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// 模板跳转链接
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
