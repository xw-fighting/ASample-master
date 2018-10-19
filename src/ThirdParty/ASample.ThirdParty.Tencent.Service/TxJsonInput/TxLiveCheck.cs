using Newtonsoft.Json;

namespace ASample.Thirdpary.Tencent.Identity.TxJsonInput
{
    /// <summary>
    /// 活体检测输入模型
    /// </summary>
    public class TxLiveCheck
    {
        /// <summary>
        /// 可取值 【1，2】（Type =1 使用腾讯证件库进行活体比对，Type =2 客户自行提供证件照片）
        /// </summary>
        [JsonProperty("live_type")]
        public string Type { get; set; }

        /// <summary>
        /// Id号(目前业务场景值身份证号 Type =1时提供)
        /// </summary>
        [JsonProperty("ID")]
        public string Id { get; set; }

        /// <summary>
        /// 姓名（用户姓名 Type =1时提供）
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 证件照片内容(填的是照片的base64编码；Type =2时提供)
        /// </summary>
        [JsonProperty("image_content")]
        public string ImageContent { get; set; }

        /// <summary>
        /// 证件照片类型(0,带网纹证件照片,1,高清证件照片,Type =2时提供)
        /// </summary>
        [JsonProperty("image_type")]
        public string ImageType { get; set; }

        /// <summary>
        /// 视频内容(活体比对视频建议时长3~6秒。video_content填的是视频的base64编码)
        /// </summary>
        [JsonProperty("video_content")]
        public string VideoContent { get; set; }

        /// <summary>
        /// 活体检测验证码
        /// </summary>
        [JsonProperty("validate_data")]
        public string ValidateData { get; set; }

        /// <summary>
        /// 活体检测等级，0是normal，1是easy，2是hard，默认是normal。(一般业务通常填1)
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }

        /// <summary>
        /// 分配的appid
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 凭据
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// 参数签名校验(md5(参数1-参数2-...-私钥key))
        /// </summary>
        [JsonProperty("sig")]
        public string Sign { get; set; }
    }
}
