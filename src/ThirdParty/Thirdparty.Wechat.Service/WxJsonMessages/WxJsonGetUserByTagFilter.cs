using Thirdparty.Wechat.Service.WxFilters;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 拉取标签下的用户的包装过滤器
    /// </summary>
    internal class WxJsonGetUserByTagFilter
    {
        [JsonProperty("tagid")]
        public string TagId { get; set; }

        [JsonProperty("next_openid")]
        public string NextOpenId { get; set; }

        public WxJsonGetUserByTagFilter(WxGetUserByTagFilter filter)
        {
            TagId = filter.TagId;
            NextOpenId = filter.NextOpenId;
        }
    }
}