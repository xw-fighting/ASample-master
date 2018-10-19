using System.Collections.Generic;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 获取用户上的标签集合
    /// </summary>
    public class WxGetidListResult
    {
        [JsonProperty("tagid_list")]
        public List<string> TagIds { get; set; }
    }
}