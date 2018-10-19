using Newtonsoft.Json;

namespace ASample.ThirdParty.WeChat.WeChatMessageSend.Model.InParam
{
    public abstract class MsgTemplateDataBasicParameter
    {
        /// <summary>
        /// 模板消息公共字段：详细请参考：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1433751277 发送模板消息
        /// </summary>
        [JsonProperty("first")]
        public TemplateDataItem First { get; set; }

        ///// <summary>
        ///// 自定义字段一
        ///// </summary>

        //[JsonProperty("keyword1")]
        //public TemplateDataItem Item1 { get; set; }

        /// <summary>
        /// 模板消息必须字段,备注信息
        /// </summary>
        [JsonProperty("remark")]
        public TemplateDataItem Remark { get; set; }
    }

    public class TemplateDataItem
    {
        /// <summary>
        /// 赋值操作
        /// </summary>
        /// <param name="value">显示的值信息</param>
        /// <param name="color">字体颜色</param>
        public TemplateDataItem(string value, string color = "#173177")
        {
            Value = value;
            Color = color;
        }
        /// <summary>
        /// 显示的值
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
