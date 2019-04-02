
namespace ASample.Logger.Models
{
    public class SystemLog
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>
        public string Level{ get; set; }

        /// <summary>
        /// 日志路径
        /// </summary>
        public string LoggerPath { get; set; }

        /// <summary>
        /// 日志提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 日志异常信息
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 日志触发程序集
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 系统标记
        /// </summary>
        public string SystemFlag{ get; set; }
    }
}
