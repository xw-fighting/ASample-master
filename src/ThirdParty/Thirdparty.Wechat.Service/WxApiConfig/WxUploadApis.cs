namespace Thirdparty.Wechat.Service.WxApiConfig
{
    public static class WxUploadApis
    {
        /// <summary>
        /// 上传媒体文件的地址
        /// </summary>
        public const string UploadMediaFileUrl = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// 上传图片、音频、视频等的接口地址
        /// </summary>
        public const string UploadFileUrl = "https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}";

        /// <summary>
        /// 上传图文消息素材的接口地址
        /// </summary>
        public const string UploadArticleUrl = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}";

        /// <summary>
        /// 下载图片素材
        /// </summary>
        public const string DownArticleUrl =
            "https://api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";
    }
}