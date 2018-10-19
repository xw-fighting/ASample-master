using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 上传图文消息里的图片的返回结果
    /// </summary>
    public class WxUpdateArticleInImageResult
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}