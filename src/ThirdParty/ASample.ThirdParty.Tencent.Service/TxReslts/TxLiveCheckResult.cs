using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxReslts
{
    /// <summary>
    /// 活体检测结果
    /// </summary>
    public class TxLiveCheckResult
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
        /// 检测信息
        /// </summary>
        [JsonProperty("data")]
        public DataSim Data { set; get; }
    }

    public class DataSim
    {
        /// <summary>
        /// 验证凭据
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// 匹配度
        /// </summary>
        [JsonProperty("sim")]
        public int Sim { get; set; }
    }
}

