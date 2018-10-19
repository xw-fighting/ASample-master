using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DRapid.Web.VisitAnalyse.Core;

namespace DRapid.Web.VisitAnalyse.Storage.SqlServer
{
    [Table("HttpVisit")]
    public class SqlHttpVisitTable : HttpVisit
    {
        public bool IsAuthenticated { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string AuthenticationType { get; set; }

        [Key]
        public override Guid Id { get; set; }

        /// <summary>
        /// 当前链接
        /// </summary>
        [MaxLength(500)]
        public override string Url { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public override DateTime Time { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        [MaxLength(10)]
        public override string Method { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        [MaxLength(10)]
        public override string StatusCode { get; set; }

        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        public override long Expires { get; set; }

        /// <summary>
        /// 资源的键
        /// </summary>
        [MaxLength(50)]
        public override string Key { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(100)]
        public override string Description { get; set; }

        /// <summary>
        /// 请求方标识符
        /// </summary>
        [NotMapped]
        public override VisitIdentity Identity { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [NotMapped]
        public override Dictionary<string, string> Inforamtion { get; set; }
    }
}
