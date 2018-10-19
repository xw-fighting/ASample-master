using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 根据openId的包装过滤器
    /// </summary>
    internal class WxJsonOpenIdFilter
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        public WxJsonOpenIdFilter(string openId)
        {
            OpenId = openId;
        }
    }
}