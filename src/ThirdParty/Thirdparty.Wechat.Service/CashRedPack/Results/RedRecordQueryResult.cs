using System.Xml.Serialization;

namespace Thirdparty.Wechat.Service.CashRedPack.Results
{
    [XmlRoot("xml")]
    public class RedRecordQueryResult
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
        /// 红包单号 ，使用API发放现金红包时返回的红包单号 
        /// </summary>
        [XmlElement("detail_id")]
        public string DetailId { get; set; }


        /// <summary>
        /// 红包状态 
        ///SENDING:发放中
        ///SENT:已发放待领取
        ///FAILED：发放失败
        ///RECEIVED:已领取
        ///RFUND_ING:退款中
        ///REFUND:已退款
        /// </summary>
        [XmlElement("status")]
        public string Status { get; set; }


        /// <summary>
        /// 发放类型 
        /// API:通过API接口发放 
        /// UPLOAD:通过上传文件方式发放
        /// ACTIVITY:通过活动方式发放
        /// </summary>
        [XmlElement("send_type")]
        public string SendType { get; set; }


        /// <summary>
        /// 红包类型 
        /// GROUP:裂变红包 
        /// NORMAL:普通红包
        /// </summary>
        [XmlElement("hb_type")]
        public string HbType { get; set; }


        /// <summary>
        /// 红包个数 
        /// </summary>
        [XmlElement("total_num")]
        public int TotalNum { get; set; }


        /// <summary>
        /// 红包金额 ，单位分
        /// </summary>
        [XmlElement("total_amount")]
        public int TotalAmount { get; set; }

        /// <summary>
        /// 失败原因 
        /// </summary>
        [XmlElement("reason")]
        public string Reason { get; set; }


        /// <summary>
        /// 红包发送时间 ，格式：2015-04-21 20:00:00 
        /// </summary>
        [XmlElement("send_time")]
        public string SendTime { get; set; }


        /// <summary>
        /// 红包的退款时间（如果其未领取的退款）  ，格式： 2015-04-21 23:03:00
        /// </summary>
        [XmlElement("refund_time")]
        public string RefundTime { get; set; }


        /// <summary>
        /// 红包退款金额 
        /// </summary>
        [XmlElement("refund_amount")]
        public string RefundAmount { get; set; }

        /// <summary>
        /// 祝福语 
        /// </summary>
        [XmlElement("wishing")]
        public string Wishing { get; set; }

        /// <summary>
        /// 活动描述 
        /// </summary>
        [XmlElement("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 裂变红包领取列表
        /// </summary>
        [XmlElement("hblist")]
        public string HbList { get; set; }

        /// <summary>
        ///领取红包的Openid
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 金额 ，领取金额 
        /// </summary>
        [XmlElement("amount")]
        public int Amount { get; set; }


        /// <summary>
        /// 接收时间，领取红包的时间
        /// </summary>
        [XmlElement("rcv_time")]
        public string RcvTime { get; set; }

        #endregion
    }
}
