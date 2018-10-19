using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 缩略图
    /// </summary>
    public class WxUpdateThumbResult : WxUpdateMediaResult
    {
        [JsonProperty("thumb_media_id")]
        public override string MediaId { get; set; }
    }
}