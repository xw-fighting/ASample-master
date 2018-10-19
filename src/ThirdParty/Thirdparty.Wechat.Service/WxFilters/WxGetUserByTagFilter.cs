namespace Thirdparty.Wechat.Service.WxFilters
{
    /// <summary>
    /// 获取用户列表的过滤器
    /// </summary>
    public class WxGetUserByTagFilter
    {
        public string TagId { get; set; }

        public string NextOpenId { get; set; }

        public WxGetUserByTagFilter(string tagId, string nextOpenId = "")
        {
            TagId = tagId;
            NextOpenId = nextOpenId;
        }
    }
}