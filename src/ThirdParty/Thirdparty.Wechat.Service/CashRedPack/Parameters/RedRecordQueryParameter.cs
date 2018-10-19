using System.Xml.Serialization;

namespace Thirdparty.Wechat.Service.CashRedPack.Parameters
{
    /// <summary>
    /// 查询红包记录接口
    /// <see cref="https://pay.weixin.qq.com/wiki/doc/api/tools/cash_coupon.php?chapter=13_6&index=5"/>
    /// </summary>
    [XmlRoot("xml")]
    public class RedRecordQueryParameter
    {
        /// <summary>
        /// 公众账号appid，微信分配的公众账号ID（企业号corpid即为此appId）。
        /// </summary>
        [XmlElement("appid", Order = 1)]
        public string WxAppId { get; set; }

        /// <summary>
        /// 订单类型 ，此处为MCHT:通过商户订单号获取红包信息。 
        /// </summary>
        [XmlElement("bill_type", Order = 2)]
        public string BillType { get; set; }

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

        /// <summary>wxappid
        /// 随机字符串
        /// </summary>
        [XmlElement("nonce_str", Order = 5)]
        public string NonceStr { get; set; }




        [XmlIgnore]
        public string MchKey { get; set; }

    }
}
