namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// 文本消息的基类
    /// </summary>
    public class WxTextMessage : IWxTextMessage
    {
        /// <summary>
        /// 消息类别
        /// </summary>
        public WxMessageTypes MessageType => WxMessageTypes.Text;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}