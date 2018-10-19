using System.Collections.Generic;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装过的上传图文消息容器
    /// </summary>
    internal class WxJsonArticlesContainer
    {
        [JsonProperty("articles")]
        public IEnumerable<WxJsonArtcle> WxArtcles { get; set; }

        public WxJsonArticlesContainer(IEnumerable<WxJsonArtcle> artcles)
        {
            WxArtcles = artcles;
        }
    }
}