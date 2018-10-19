using Thirdparty.Wechat.Service.WxFilters;

namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// tag发送的图文消息
    /// </summary>
    public class WxTagImageTextMessage : IWxTagMessage,IWxImageTextMessageContainer
    {
        public WxMessageTypes MessageType => WxMessageTypes.Mpnews;

        public IWxTagFilter TagFilter { get; set; }

        public IWxImageTextMessage ImageTextMessage { get; set; }
    };
}