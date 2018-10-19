using System.IO;

namespace Thirdparty.Wechat.Service.WxResults
{
    public class WxDownBaseArticleResult
    {
        public Stream ArticleStream { get; set; }

        public WxBaseResult WxResult { get; set; }
    }
}