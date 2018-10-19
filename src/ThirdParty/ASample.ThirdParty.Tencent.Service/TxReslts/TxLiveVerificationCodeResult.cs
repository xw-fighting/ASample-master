using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxReslts
{
    /// <summary>
    /// 活体验证码结果
    /// </summary>
    public class TxLiveVerificationCodeResult
    {
        /// <summary>
        /// 返回状态码,0表示成功，非0值为出错
        /// </summary>
        [JsonProperty("errorcode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 返回错误描述
        /// </summary>
        [JsonProperty("errormsg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 验证码数字
        /// </summary>
        [JsonProperty("data")]
        public DataValidate Data { set; get; }

        public class DataValidate
        {
            /// <summary>
            /// 验证码数字
            /// </summary>
            [JsonProperty("validate_data")]
            public string ValidateData { get; set; }
        }
    }
}
