using Thirdparty.Wechat.Service.WxFilters;

namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// custom模式下的文本消息
    /// </summary>
    public class WxCustomTextMessage : IWxCustomMessage, IWxTextMessageContainer
    {
        public IWxCustomFilter CustomFilter { get; set; }

        public WxMessageTypes MessageType => WxMessageTypes.Text;

        public IWxTextMessage TextMessage { get; set; }
    }
}