using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxReslts
{
    /// <summary>
    /// 实名登录返回结果
    /// </summary>
    public class TxRealNameLoginResult
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
        /// 凭据信息
        /// </summary>
        [JsonProperty("data")]
        public DataToken Data { get; set; }
    }

    /// <summary>
    /// 验证结果
    /// </summary>
    public class DataToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }


}
