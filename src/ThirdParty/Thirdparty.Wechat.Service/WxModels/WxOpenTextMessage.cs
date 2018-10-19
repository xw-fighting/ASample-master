using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxFilters;

namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// Open模式下的文本消息
    /// </summary>
    public class WxOpenTextMessage : IWxOpenMessage, IWxTextMessageContainer
    {
        /// <summary>
        /// 消息类别
        /// </summary>
        public WxMessageTypes MessageType => WxMessageTypes.Text;
 
        public IWxOpenFilter OpenFilter { get; set; }

        public IWxTextMessage TextMessage { get; set; }
    }
}