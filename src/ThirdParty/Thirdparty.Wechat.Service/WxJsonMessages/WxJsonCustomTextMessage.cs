using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装过的文本信息
    /// </summary>
    internal class WxJsonCustomTextMessage : WxJsonTextMessage
    {
        public WxJsonCustomTextMessage(WxCustomTextMessage customTextMessage)
            : base(customTextMessage.TextMessage)
        {
            OpenId = customTextMessage.CustomFilter.OpenId;
        }

        [JsonProperty("touser")]
        public string OpenId { get; set; }
    }
}