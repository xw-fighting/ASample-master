using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Thirdparty.Wechat.Service.CashRedPack.Parameters.Reuqest
{
    /// <summary>
    /// 发送现金红包 - 裂变(组)红包
    /// <see cref="https://pay.weixin.qq.com/wiki/doc/api/tools/cash_coupon.php?chapter=13_1"/>
    /// </summary>
    [XmlRoot("xml")]
    public class GroupRedPackRequestParam : GroupRedPackParameter, IWxSafe
    {

        public GroupRedPackRequestParam()
        {
        }

        public GroupRedPackRequestParam(GroupRedPackParameter baseParam)
        {
            ActName = baseParam.ActName;
            AmtType = baseParam.AmtType;
            //ClientIp = baseParam.ClientIp;
            MchBillno = baseParam.MchBillno;
            MchId = baseParam.MchId;
            ReOpenId = baseParam.ReOpenId;
            Remark = baseParam.Remark;
            SceneId = baseParam.SceneId;
            SendName = baseParam.SendName;
            TotalAmount = baseParam.TotalAmount;
            TotalNum = baseParam.TotalNum;
            Wishing = baseParam.Wishing;
            WxAppId = baseParam.WxAppId;

            MchKey = baseParam.MchKey;
            RiskInfo = baseParam.RiskInfo;
            ConsumeMchId = baseParam.ConsumeMchId;

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
                {"wxappid", WxAppId },
                {"send_name", SendName },
                {"re_openid", ReOpenId },
                {"total_amount", TotalAmount.ToString() },
                {"total_num", TotalNum.ToString() },
                {"amt_type", AmtType },
                {"wishing", Wishing },
                {"act_name", ActName },
                {"remark", Remark },
                {"scene_id", SceneId },
                {"risk_info", RiskInfo },
                {"consume_mch_id", ConsumeMchId }
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
