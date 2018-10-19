using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    public class WxTemplateResult : WxBaseResult
    {
        [JsonProperty("msgid")]
        public string MsgId { get; set; }
    }
}