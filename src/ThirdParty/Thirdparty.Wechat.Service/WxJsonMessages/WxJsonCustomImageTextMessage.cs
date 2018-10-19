using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装 custom下的图文消息
    /// </summary>
    internal class WxJsonCustomImageTextMessage : WxJsonImageTextMessage
    {
        public WxJsonCustomImageTextMessage(WxCustomImageTextMessage message) 
            : base(message.ImageTextMessage)
        {
            OpenId = message.CustomFilter.OpenId;
        }

        [JsonProperty("touser")]
        public string OpenId { get; set; }
    }
}