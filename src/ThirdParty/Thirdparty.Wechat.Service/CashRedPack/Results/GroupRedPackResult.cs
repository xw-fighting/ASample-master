using System.Xml.Serialization;

namespace Thirdparty.Wechat.Service.CashRedPack.Results
{
    [XmlRoot("xml")]
    public class GroupRedPackResult
    {
        /// <summary>
        /// 返回状态码
        /// SUCCESS/FAIL  此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 返回信息
        /// 返回信息，如非空，为错误原因 签名失败 参数格式校验错误
        /// </summary>
        [XmlElement("return_msg")]
        public string ReturnMsg { get; set; }


        #region 以下字段在return_code为SUCCESS的时候有返回

        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 业务结果，SUCCESS/FAIL
        /// </summary>
        [XmlElement("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElement("err_code")]
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        [XmlElement("err_code_des")]
        public string ErrDodeDes { get; set; }

        #endregion

        #region 以下字段在return_code和result_code都为SUCCESS的时候有返回

        /// <summary>
        /// 商户订单号（每个订单号必须唯一） 
        /// </summary>
        [XmlElement("mch_billno")]
        public string MchBillNo { get; set; }

        /// <summary>
        /// 商户号（微信支付分配的商户号）
        /// </summary>
        [XmlElement("mch_id")]
        public string MchId { get; set; }

        /// <summary>
        /// 公众账号appid （商户appid，接口传入的所有appid应该为公众号的appid）
        /// </summary>
        [XmlElement("wxappid")]
        public string WxAppId { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        [XmlElement("re_openid")]
        public string ReOpenId { get; set; }

        /// <summary>
        /// 付款金额，单位分
        /// </summary>
        [XmlElement("total_amount")]
        public int TotalAmount { get; set; }

        /// <summary>
        /// 微信单号，红包订单的微信单号
        /// </summary>
        [XmlElement("send_listid")]
        public string SendListid { get; set; }

        #endregion
    }
}
