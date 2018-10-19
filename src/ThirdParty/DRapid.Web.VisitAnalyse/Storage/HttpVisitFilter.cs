using System;

namespace DRapid.Web.VisitAnalyse.Storage
{
    /// <summary>
    /// 访问记录的过滤条件
    /// </summary>
    public class HttpVisitFilter
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 过滤的开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 过滤的结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 响应状态码
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 响应时间过滤的最小值
        /// </summary>
        public int MinExpires { get; set; }

        /// <summary>
        /// 响应时间过滤的最大值
        /// </summary>
        public int MaxExpires { get; set; }

        /// <summary>
        /// 本次请求的功能性编码
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 本次请求的功能性描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 当前请求的用户名
        /// </summary>
        public string UserName { get; set; }
    }
}
