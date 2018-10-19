using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    public class WxJsonTemplateKeyword
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public WxJsonTemplateKeyword() { }

        public WxJsonTemplateKeyword(WxTemplateKeyword wxTemplateKeyword)
        {
            Color = wxTemplateKeyword.Color;
            Value = wxTemplateKeyword.Value;
        }
    }
}