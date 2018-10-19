using System.Collections.Generic;

namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// 图文消息基类
    /// </summary>
    public class WxImageTextMessage : IWxImageTextMessage
    {
        /// <summary>
        /// 素材Id
        /// </summary>
        public string MediaId { get; set; }

        public WxMessageTypes MessageType => WxMessageTypes.Mpnews;

        /// <summary>
        /// 原创校验
        /// </summary>
        public int Reprint { get; set; }
    }
}