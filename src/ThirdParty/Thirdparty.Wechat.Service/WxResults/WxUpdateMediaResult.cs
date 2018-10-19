using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 上传图文消息素材的返回结果
    /// </summary>
    public class WxUpdateMediaResult
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("media_id")]
        public virtual string MediaId { get; set; }

        [JsonProperty("created_at")]
        public string CreateTime { get; set; }
    }
}