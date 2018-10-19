using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Thirdparty.Wechat.Service.CashRedPack.Parameters.Reuqest
{
    /// <summary>
    /// 发送现金红包 - 普通红包
    /// <see cref="https://pay.weixin.qq.com/wiki/doc/api/tools/cash_coupon.php?chapter=13_1"/>
    /// </summary>
    [XmlRoot("xml")]
    public class GeneralRedPackRequestParam : GeneralRedPackParameter, IWxSafe
    {
        public GeneralRedPackRequestParam()
        {

        }
        public GeneralRedPackRequestParam(WxCashRedPackConfig wxCashRedPackConfig)
        {
            MchId = wxCashRedPackConfig.MchId;
            WxAppId = wxCashRedPackConfig.WxAppId;
            MchKey = wxCashRedPackConfig.MchKey;
            SendName = wxCashRedPackConfig.SendName;
            TotalNum = 1; //普通红包一次只能一个
            ClientIp = wxCashRedPackConfig.ClientIp;
            ConsumeMchId = wxCashRedPackConfig.ConsumeMchId;
        }

        public GeneralRedPackRequestParam(GeneralRedPackParameter baseParam)
        {
            MchBillno = baseParam.MchBillno;
            MchId = baseParam.MchId;
            WxAppId = baseParam.WxAppId;
            MchKey = baseParam.MchKey;
            SendName = baseParam.SendName;
            ReOpenId = baseParam.ReOpenId;
            TotalAmount = baseParam.TotalAmount;
            TotalNum = 1; //普通红包一次只能一个
            Wishing = baseParam.Wishing;
            ClientIp = baseParam.ClientIp;
            ActName = baseParam.ActName;
            Remark = baseParam.Remark;
            SceneId = baseParam.SceneId;
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
                {"wishing", Wishing },
                {"client_ip", ClientIp },
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
