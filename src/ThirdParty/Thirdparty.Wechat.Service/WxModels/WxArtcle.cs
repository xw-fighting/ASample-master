namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// 图文素材
    /// </summary>
    public class WxArtcle
    {
        /// <summary>
        /// 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
        /// </summary>
        public string ThumbMediaId { set; get; }

        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string Author { set; get; }

        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        public string ContentSourceUrl { set; get; }

        /// <summary>
        /// 图文消息页面的内容，支持HTML标签
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string Digest { set; get; }

        /// <summary>
        /// 是否显示封面，1为显示，0为不显示
        /// </summary>
        public int ShowCoverPic { set; get; }
    }
}