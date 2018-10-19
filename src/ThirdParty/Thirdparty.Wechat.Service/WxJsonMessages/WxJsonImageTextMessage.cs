using System.Collections.Generic;
using Thirdparty.Wechat.Service.WxModels;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxJsonMessages
{
    /// <summary>
    /// 包装过的图文消息
    /// </summary>
    internal class WxJsonImageTextMessage : WxJsonMessage
    {
        public WxJsonImageTextMessage(IWxImageTextMessage message)
        {
            Body = new WxJsonImageTextMessageBody { MediaId = message.MediaId };
            Reprint = message.Reprint;
            MessageType = message.MessageType.ToString().ToLower();
        }

        [JsonProperty("mpnews")]
        public WxJsonImageTextMessageBody Body { get;   set; }
    }

    internal class WxJsonImageTextMessageBody
    {
        [JsonProperty("media_id")]
        public string MediaId { get; set; }
    }
}