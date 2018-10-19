using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxModels;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 获取标签集合结构体
    /// </summary>
    public class WxTagListResult
    {
        public List<WxTag> Tags { get; set; }
    }
}