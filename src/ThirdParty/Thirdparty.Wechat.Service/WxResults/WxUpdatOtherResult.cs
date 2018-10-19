using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 其他上传的返回结果
    /// </summary>
    public class WxUpdatOtherResult
    {
        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}