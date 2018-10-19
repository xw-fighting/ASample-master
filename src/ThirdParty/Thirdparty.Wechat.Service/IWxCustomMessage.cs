using Thirdparty.Wechat.Service.WxFilters;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// Custom消息的实现接口
    /// </summary>
    public interface IWxCustomMessage : IWxMessage, IWxCustomFilterContainer
    {

    }
}