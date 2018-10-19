using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 根据标签获取用户列表
    /// </summary>
    public class WxSelectUserByTagResult
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("data")]
        public WxJsonOpenList WxJsonOpenList { get; set; }

        [JsonProperty("next_openid")]
        public string NextOpenId { get; set; }

        public WxUserPage GetUserPage()
        {
            return new WxUserPage
            {
                Count = Count,
                NextOpenId = NextOpenId,
                OpenIds = WxJsonOpenList.OpenIds
            };
        }
    }

    public class WxJsonOpenList
    {
        [JsonProperty("openid")]
        public List<string> OpenIds { get; set; }
    }
}