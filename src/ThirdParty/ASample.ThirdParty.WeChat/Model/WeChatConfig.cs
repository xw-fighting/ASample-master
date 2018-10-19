
namespace ASample.ThirdParty.WeChat.Model
{
    public class WeChatConfig 
    {
        /// <summary>
        /// 第三方用户唯一凭证
        /// </summary>
        public string WxAppid { get; set; }

        /// <summary>
        /// 第三方用户唯一凭证密钥，即appsecret
        /// </summary>
        public string WxSecret { get; set; }

        /// <summary>
        /// 微信模板编号缩写
        /// </summary>
        public string WxMessageTemplateId { get; set; }

        /// <summary>
        /// 微信发送审核通知消息模板编号
        /// </summary>
        public string WxCheckMessageTemplateId { get; set; }

        /// <summary>
        /// 腾讯人脸识别第三方凭证
        /// </summary>
        public string TencentAppid { get; set; }



        /// <summary>
        /// 腾讯人脸识别第三方凭证秘钥
        /// </summary>
        public string TencentSecret { get; set; }

        /// <summary>
        /// 腾讯人脸识别第三方验证私钥
        /// </summary>
        public string TencentSignKey { get; set; }

        /// <summary>
        /// 腾讯人脸识别签名有效时间
        /// </summary>
        public int TencentExpired { get; set; }

        /// <summary>
        /// 腾讯人脸识别匹配率
        /// </summary>
        public int TencentSim { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WS { get; set; }

        #region WebServiceAPI

        /// <summary>
        /// 
        /// </summary>
        public string WSKey { get; set; }

        /// <summary>
        /// API文档地址http://lbs.qq.com/webservice_v1/guide-gcoder.html
        /// </summary>
        public string WSGeocoderUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WSGeocoderCoordType { get; set; }

        public string WxApiTokenGetUrl { get; set; }

        public string WxApiTicketGetUrl { get; set; }

        #endregion
    }
}
