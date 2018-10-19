using System.Xml.Serialization;

namespace Thirdparty.Wechat.Service.CashRedPack.Parameters
{
    /// <summary>
    /// (现金红包)发送普通红包参数
    /// <see cref="https://pay.weixin.qq.com/wiki/doc/api/tools/cash_coupon.php?chapter=13_1"/>
    /// </summary>
    public class GeneralRedPackParameter
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        [XmlElement("act_name", Order = 1)]
        public string ActName { get; set; }
 
        /// <summary>
        /// Ip地址（调用接口的机器Ip地址）
        /// </summary>
        [XmlElement("client_ip", Order = 2)]
        public string ClientIp { get; set; }

        /// <summary>
        /// 商户订单号(每个订单号必须唯一)
        /// </summary>
        [XmlElement("mch_billno", Order = 3)]
        public string MchBillno { get; set; }

        /// <summary>
        /// 商户号(微信支付分配的商户号)
        /// </summary>
        [XmlElement("mch_id", Order = 4)]
        public string MchId { get; set; }

        //nonce_str , Order = 5

        /// <summary>
        /// 随机字符串
        /// </summary>
        [XmlElement("nonce_str", Order = 5)]
        public string NonceStr { get; set; }

        /// <summary>
        /// 用户openid（接受红包的用户）
        /// </summary>
        [XmlElement("re_openid", Order = 6)]
        public string ReOpenId { get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("remark", Order = 7)]
        public string Remark { get; set; }

        /// <summary>
        /// 场景id（发放红包使用场景，红包金额大于200时必传）
        /// </summary>
        [XmlElement("scene_id", Order = 8)]
        public string SceneId { get; set; }
 
        /// <summary>
        /// 商户名称（红包发送者名称）
        /// </summary>
        [XmlElement("send_name", Order = 9)]
        public string SendName { get; set; }
 
        /// <summary>
        /// 付款金额（付款金额，单位分）
        /// </summary>
        [XmlElement("total_amount", Order = 10)]
        public int TotalAmount { get; set; }
 
        //total_num , Order = 11
        /// <summary>
        /// 红包发放总人数（红包发放总人数 total_num=1）
        /// </summary>
        [XmlElement("total_num", Order = 11)]
        public int TotalNum { get; set; }
 
        /// <summary>
        /// 红包祝福语
        /// </summary>
        [XmlElement("wishing", Order = 12)]
        public string Wishing { get; set; }
 
        /// <summary>
        /// 公众账号appid，微信分配的公众账号ID（企业号corpid即为此appId）。
        /// </summary>
        [XmlElement("wxappid", Order = 13)]
        public string WxAppId { get; set; }

        /// <summary>
        /// 公众账号密钥
        /// </summary>
        [XmlIgnore]
        public string MchKey { get; set; }
 
        /// <summary>
        /// 活动信息
        /// </summary>
        //[XmlElement("risk_info")]
        [XmlIgnore]
        public string RiskInfo { get; set; }

        /// <summary>
        /// 资金授权商户号
        /// </summary>
        //[XmlElement("consume_mch_id")]
        [XmlIgnore]
        public string ConsumeMchId { get; set; }
    }
}
