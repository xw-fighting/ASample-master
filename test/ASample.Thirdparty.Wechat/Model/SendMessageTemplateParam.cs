using ASample.ThirdParty.WeChat.WeChatMessageSend.Model.InParam;
using Newtonsoft.Json;

namespace ASample.Thirdparty.Wechat.Test.Model
{
    public class SendMessageTemplateParam : MsgTemplateDataBasicParameter
    {
        [JsonProperty("keyword1")]
        public TemplateDataItem Item1 { get; set; }

        [JsonProperty("keyword2")]
        public TemplateDataItem Item2 { get; set; }
    }
}
