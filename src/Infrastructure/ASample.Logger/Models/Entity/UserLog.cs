
namespace ASample.Logger.Models
{
    public class UserLog
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 来源平台
        /// </summary>
        public string SourcePlatform { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务编码
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// 用户访问时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 日志来源系统
        /// </summary>
        public string SystemFlag { get; set; }
    }
}
