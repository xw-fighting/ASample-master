using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装过的图文素材
    /// </summary>
    internal class WxJsonArtcle
    {
        public WxJsonArtcle(WxArtcle artcle)
        {
            ThumbMediaId = artcle.ThumbMediaId;
            Author = artcle.Author;
            Title = artcle.Title;
            Content = artcle.Content;
            ContentSourceUrl = artcle.ContentSourceUrl;
            Digest = artcle.Digest;
            ShowCoverPic = artcle.ShowCoverPic;
        }

        /// <summary>
        /// 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
        /// </summary>
        [JsonProperty("thumb_media_id")]
        public string ThumbMediaId { set; get; }

        /// <summary>
        /// 图文消息的作者
        /// </summary>
        [JsonProperty("author")]
        public string Author { set; get; }

        /// <summary>
        /// 图文消息的标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { set; get; }

        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        [JsonProperty("content_source_url")]
        public string ContentSourceUrl { set; get; }

        /// <summary>
        /// 图文消息页面的内容，支持HTML标签
        /// </summary>
        [JsonProperty("content")]
        public string Content { set; get; }

        /// <summary>
        /// 图文消息的描述
        /// </summary>
        [JsonProperty("digest")]
        public string Digest { set; get; }

        /// <summary>
        /// 是否显示封面，1为显示，0为不显示
        /// </summary>
        [JsonProperty("show_cover_pic")]
        public int ShowCoverPic { set; get; }
    }
}