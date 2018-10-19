using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装open模式下的文本信息
    /// </summary>
    internal class WxJsonOpenTextMessage : WxJsonTextMessage
    {
        public WxJsonOpenTextMessage(WxOpenTextMessage message)
            : base(message.TextMessage)
        {
            OpenIds = message.OpenFilter.OpenIds;
        }

        [JsonProperty("touser")]
        public List<string> OpenIds { get; set; }
    }
}