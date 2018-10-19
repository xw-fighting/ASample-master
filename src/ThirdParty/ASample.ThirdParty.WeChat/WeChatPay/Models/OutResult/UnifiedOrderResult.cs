using System.Xml.Serialization;

namespace ASample.ThirdParty.WeChat.WeChatPay.Models.OutResult
{
    [XmlRoot("xml")]
    public class UnifiedOrderResult
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>

        [XmlElement("return_msg")]
        public string ReturnMsg { get; set; }

        //以下字段在return_code为SUCCESS的时候有返回

        /// <summary>
        /// 公众账号ID
        /// </summary>
        [XmlElement("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [XmlElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [XmlElement("nonce_str")]
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 业务结果
        /// </summary>
        [XmlElement("result_code")]
        public string ResultCode { get; set; }

        //以下字段在return_code 和result_code都为SUCCESS的时候有返回

        /// <summary>
        /// 交易类型
        /// </summary>

        [XmlElement("trade_type")]
        public string TradeType { get; set; }

        /// <summary>
        /// 预支付交易会话标识
        /// </summary>

        [XmlElement("prepay_id")]
        public string PrepayId { get; set; }

        /// <summary>
        /// 二维码链接
        /// </summary>

        [XmlElement("code_url")]
        public string CodeUrl { get; set; }

        
    }
}
