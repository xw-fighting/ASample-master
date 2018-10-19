

using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxReslts
{
    /// <summary>
    /// 活体检测信息结果
    /// </summary>
    public class TxLiveCheckInfoResult
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
        /// 图片采用链接
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
