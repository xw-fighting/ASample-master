using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxFilters;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 批量打标签的包装过滤器
    /// </summary>
    internal class WxJsonBatchFilter
    {
        public WxJsonBatchFilter(WxBatchFilter batchFilter)
        {
            TagId = batchFilter.TagId;
            OpenIds = batchFilter.OpenIds;
        }

        [JsonProperty("tagid")]
        public string TagId { get; set; }

        [JsonProperty("openid_list")]
        public List<string> OpenIds { get; set; }
    }
}