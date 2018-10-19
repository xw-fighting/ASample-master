using System.Collections.Generic;

namespace Thirdparty.Wechat.Service.WxFilters
{
    public class WxOpenFilter: IWxOpenFilter
    {
        public List<string> OpenIds { get; set; }
    }
}