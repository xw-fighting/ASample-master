using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装 open 模式下的图文消息
    /// </summary>
    internal class WxJsonOpenImageTextMessage : WxJsonImageTextMessage
    {
        public WxJsonOpenImageTextMessage(WxOpenImageTextMessage message)
            :base(message.ImageTextMessage)
        {    
            OpenIds = message.OpenFilter.OpenIds;
        }

        [JsonProperty("touser")]
        public List<string> OpenIds { get; set; }
    }
}