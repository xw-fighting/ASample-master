using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Thirdparty.Wechat.Service.CashRedPack.Parameters.Reuqest
{
    /// <summary>
    /// 查询红包记录接口
    /// <see cref="https://pay.weixin.qq.com/wiki/doc/api/tools/cash_coupon.php?chapter=13_6&index=5"/>
    /// </summary>
    [XmlRoot("xml")]
    public class RedRecordQueryRequestParam : RedRecordQueryParameter, IWxSafe
    {
        public RedRecordQueryRequestParam()
        {
        }

        public RedRecordQueryRequestParam(RedRecordQueryParameter baseParam)
        {
            MchBillno = baseParam.MchBillno;
            MchId = baseParam.MchId;
            WxAppId = baseParam.WxAppId;
            MchKey = baseParam.MchKey;
            BillType = baseParam.BillType;
        }
 
        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign", Order = 100)]
        public string Sign { get; set; }


        public string ToSortedParamString()
        {
            var paramDic = new Dictionary<string, string>()
            {
                {"nonce_str", NonceStr },
                {"mch_billno", MchBillno },
                {"mch_id", MchId },
                {"appid", WxAppId },
                {"bill_type", BillType  }
            };

            var list = paramDic.ToList().Where(e => !string.IsNullOrWhiteSpace(e.Value)).Select(e => $"{e.Key}={e.Value}").ToList();
            list.Sort();
            var result = string.Join("&", list);
            return result;
        }

        public override string ToString()
        {
            return ToSortedParamString();
        }

    }
}
