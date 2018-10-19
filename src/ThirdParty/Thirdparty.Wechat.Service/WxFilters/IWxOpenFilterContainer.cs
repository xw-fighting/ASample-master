using System.Collections.Generic;

namespace Thirdparty.Wechat.Service.WxFilters
{
    public interface IWxOpenFilterContainer
    {
        IWxOpenFilter OpenFilter { get; set; }
    }

    public interface IWxOpenFilter
    {
        List<string> OpenIds { get; set; }
    }
}