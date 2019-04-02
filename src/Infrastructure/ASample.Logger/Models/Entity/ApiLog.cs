
namespace ASample.Logger.Models
{
    public class ApiLog
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 开始访问时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 接口耗时
        /// </summary>
        public string ElapsedTime { get; set; }

        /// <summary>
        /// 接口消息提示
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 接口异常信息
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 接口异常来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 接口访问页面来源
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// 接口日志创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 日志来源系统
        /// </summary>
        public string SystemFlagd { get; set; }
    }
}
