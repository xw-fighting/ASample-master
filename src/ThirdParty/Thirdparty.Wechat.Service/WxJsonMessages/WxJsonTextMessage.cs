using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装 文本消息
    /// </summary>
    internal class WxJsonTextMessage : WxJsonMessage
    {
        [JsonProperty("text")]
        public WxJsonTextMessageBody Body { get; private set; }

        public WxJsonTextMessage(IWxTextMessage message)
        {
            Body = new WxJsonTextMessageBody
            {
                Content = message.Content
            };

            MessageType = message.MessageType.ToString().ToLower();
        }
    }

    public class WxJsonTextMessageBody
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}