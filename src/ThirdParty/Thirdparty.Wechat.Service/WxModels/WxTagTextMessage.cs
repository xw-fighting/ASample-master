using Thirdparty.Wechat.Service.WxFilters;
using Thirdparty.Wechat.Service.WxJsonMessages;

namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// tag模式下的文本消息
    /// </summary>
    public class WxTagTextMessage : IWxTagMessage, IWxTextMessageContainer
    {
        public WxMessageTypes MessageType => WxMessageTypes.Text;

        public IWxTagFilter TagFilter { get; set; }

        public IWxTextMessage TextMessage { get; set; }
    }
}