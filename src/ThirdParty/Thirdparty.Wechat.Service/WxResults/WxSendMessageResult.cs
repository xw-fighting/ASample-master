﻿using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    /// <summary>
    /// 发送消息的返回结构体
    /// </summary>
    public class WxSendMessageResult : WxBaseResult
    {
        /// <summary>
        /// 消息发送任务的ID
        /// </summary>
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        /// <summary>
        /// 消息的数据ID，，该字段只有在群发图文消息时，才会出现。
        /// 可以用于在图文分析数据接口中，获取到对应的图文消息的数据，
        /// 是图文分析数据接口中的msgid字段中的前半部分，详见图文分析数据接口中的msgid字段的介绍。
        /// </summary>
        [JsonProperty("msg_data_id")]
        public string MsgDataId { get; set; }
    }
}