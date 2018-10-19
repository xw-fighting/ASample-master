using System.Collections.Generic;

namespace Thirdparty.Wechat.Service.WxModels
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public class WxUserPage
    {
        public long Count { get; set; }

        public List<string> OpenIds { get; set; }

        public string NextOpenId { get; set; }
    }
}