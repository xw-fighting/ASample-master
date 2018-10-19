
using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxReslts
{
    /// <summary>
    /// 获取检测人的信息
    /// </summary>
    public class TxLiveCheckUserInfoResult
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [JsonProperty("ID")]
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sex")]
        public string Sex { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [JsonProperty("nation")]
        public string Nation { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [JsonProperty("ID_address")]
        public string Address { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [JsonProperty("ID_birth")]
        public string BirthDay { get; set; }

        /// <summary>
        /// 当地权威机构
        /// </summary>
        [JsonProperty("ID_authority")]
        public string Authority { get; set; }

        /// <summary>
        /// 有效日期
        /// </summary>
        [JsonProperty("ID_valid_date")]
        public string ValidDate { get; set; }

        [JsonProperty("validatedata")]
        public string ValidDateData { get; set; }

        /// <summary>
        /// 身份证正面照片的base64编码
        /// </summary>
        [JsonProperty("frontpic")]
        public string FrontPic { get; set; }

        /// <summary>
        /// 身份证反面照片的base64编码
        /// </summary>
        [JsonProperty("backpic")]
        public string BackPic { get; set; }

        /// <summary>
        /// 视频截图1
        /// </summary>
        [JsonProperty("videopic1")]
        public string VideoPic1 { get; set; }

        /// <summary>
        /// 视频截图2
        /// </summary>
        [JsonProperty("videopic2")]
        public string VideoPic2 { get; set; }

        /// <summary>
        /// 视频截图3
        /// </summary>
        [JsonProperty("videopic3")]
        public string VideoPic3 { get; set; }

        /// <summary>
        /// 视频的base64编码
        /// </summary>
        [JsonProperty("video")]
        public string Video { get; set; }

        ///// <summary>
        ///// 返回状态码,0表示成功，非0值为出错
        ///// </summary>
        //[JsonProperty("yt_errorcode")]
        //public int ErrorCode { get; set; }

        ///// <summary>
        ///// 返回错误描述
        ///// </summary>
        //[JsonProperty("yt_errormsg")]
        //public string ErrorMsg { get; set; }

        /// <summary>
        /// 活体状态
        /// </summary>
        [JsonProperty("livestatus")]
        public string LiveStatus { get; set; }

        /// <summary>
        /// 活体信息
        /// </summary>
        [JsonProperty("livemsg")]
        public string LiveMsg { get; set; }

        /// <summary>
        /// 对比状态
        /// </summary>
        [JsonProperty("comparestatus")]
        public string CompareStatus { get; set; }

        /// <summary>
        /// 对比信息
        /// </summary>
        [JsonProperty("comparemsg")]
        public string CompareMsg { get; set; }

        /// <summary>
        /// 匹配度
        /// </summary>
        [JsonProperty("sim")]
        public string Sim { get; set; }

        /// <summary>
        /// 默认为0（即首次实名验证）
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 检测状态
        /// </summary>
        public string DetectStatus { get; set; }

        /// <summary>
        /// 检测结果
        /// </summary>
        public string DetectMsg { get; set; }

        /// <summary>
        /// 人脸识别Id
        /// </summary>
        public string FaceRecognitionId { get; set; }

    }
}
