using System;
using System.Collections.Generic;

namespace DRapid.Web.VisitAnalyse.Core
{
    /// <summary>
    /// 系统访问记录
    /// </summary>
    public class HttpVisit : ICloneable
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 当前链接
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public virtual DateTime Time { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public virtual string Method { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public virtual string StatusCode { get; set; }

        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        public virtual long Expires { get; set; }

        /// <summary>
        /// 资源的键
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 请求方标识符
        /// </summary>
        public virtual VisitIdentity Identity { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public virtual Dictionary<string, string> Inforamtion { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}