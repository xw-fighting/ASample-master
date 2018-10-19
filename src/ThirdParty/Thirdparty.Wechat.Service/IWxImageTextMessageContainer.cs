using Thirdparty.Wechat.Service.WxModels;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// 图文消息的接口
    /// </summary>
    public interface IWxImageTextMessageContainer
    {
        IWxImageTextMessage ImageTextMessage { get; }
    }
}