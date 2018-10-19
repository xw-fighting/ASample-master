using Thirdparty.Wechat.Service.WxFilters;

namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// custom模式下的图文消息
    /// </summary>
    public class WxCustomImageTextMessage : IWxCustomMessage, IWxImageTextMessageContainer
    {
        public IWxCustomFilter CustomFilter { get; set; }

        public WxMessageTypes MessageType => WxMessageTypes.Mpnews;

        public IWxImageTextMessage ImageTextMessage { get; set; }
    }
}