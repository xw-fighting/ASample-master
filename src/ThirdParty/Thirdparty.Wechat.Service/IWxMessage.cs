using Thirdparty.Wechat.Service.WxModels;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// 消息基类
    /// </summary>
    public interface IWxMessage
    {
        WxMessageTypes MessageType { get; }
    }
}