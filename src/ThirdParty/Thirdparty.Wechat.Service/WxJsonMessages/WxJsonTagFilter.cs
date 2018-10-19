using Thirdparty.Wechat.Service.WxFilters;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装tag模式下的过滤器
    /// </summary>
    internal class WxJsonTagFilter
    {
        [JsonProperty("is_to_all")]
        public bool IsAll { get; set; }

        [JsonProperty("tag_id")]
        public string TagId { get; set; }

        public WxJsonTagFilter(IWxTagFilter tagFilter)
        {
            IsAll = tagFilter.IsAll;
            TagId = tagFilter.TagId;
        }
    }


}