using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装 tag模式下的文本消息
    /// </summary>
    internal class WxJsonTagTextMessage : WxJsonTextMessage
    {
        public WxJsonTagTextMessage(WxTagTextMessage message)
            : base(message.TextMessage)
        {
            WxJsonTagFilter = new WxJsonTagFilter(message.TagFilter);
        }

        [JsonProperty("filter")]
        public WxJsonTagFilter WxJsonTagFilter { get; set; }
    }
}