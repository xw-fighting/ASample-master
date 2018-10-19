using System.Collections.Generic;

namespace Thirdparty.Wechat.Service.WxFilters
{
    /// <summary>
    /// 批量打tag 的过滤器
    /// </summary>
    public class WxBatchFilter
    {
        public string TagId { get; set; }

        public List<string> OpenIds { get; set; }

        public WxBatchFilter(string tagId, List<string> openIds)
        {
            TagId = tagId;
            OpenIds = openIds;
        }
    }
}