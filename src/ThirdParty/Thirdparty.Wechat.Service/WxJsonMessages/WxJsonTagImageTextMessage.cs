using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装 tag模式下的图文消息
    /// </summary>
    internal class WxJsonTagImageTextMessage : WxJsonImageTextMessage
    {
        public WxJsonTagImageTextMessage(WxTagImageTextMessage message) 
            : base(message.ImageTextMessage)
        {
            WxJsonTagFilter = new WxJsonTagFilter(message.TagFilter);
        }

        [JsonProperty("filter")]
        public WxJsonTagFilter WxJsonTagFilter { get; set; }
    }
}