using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxFilters;

namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// Open模式下的图文消息
    /// </summary>
    public class WxOpenImageTextMessage : IWxOpenMessage, IWxImageTextMessageContainer
    {
        public WxMessageTypes MessageType => WxMessageTypes.Mpnews;

        public IWxOpenFilter OpenFilter { get; set; }

        public IWxImageTextMessage ImageTextMessage { get; set; }
    }
}