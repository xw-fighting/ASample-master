using Thirdparty.Wechat.Service.WxModels;

namespace Thirdparty.Wechat.Service
{
    public interface IWxTextMessageContainer  
    {
        IWxTextMessage TextMessage { get; }
    }
}