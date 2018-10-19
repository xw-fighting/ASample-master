using System.Collections.Generic;

namespace Thirdparty.Wechat.Service.WxFilters
{
    /// <summary>
    /// 按标签发的头
    /// </summary>
    public class WxTagFilter : IWxTagFilter
    {
        /// <summary>
        /// 群发
        /// </summary>
        public bool IsAll { get; private set; }

        /// <summary>
        /// 标签号，IsAll为True的时候， TagId可不填
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// 创建推送给全部的
        /// </summary>
        /// <returns></returns>
        public static WxTagFilter CreateAll()
        {
            return new WxTagFilter
            {
                IsAll = true
            };
        }

        private WxTagFilter()
            : this("")
        {

        }

        /// <summary>
        /// 推送给指定标签的
        /// </summary>
        /// <param name="tagId"></param>
        public WxTagFilter(string tagId)
        {
            TagId = tagId;
        }
    }
}