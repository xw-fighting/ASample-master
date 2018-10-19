using ASample.Web.Common.Excel.Values;
using System;

namespace ASample.Web.Common.Excel.Models
{
    /// <summary>
    /// Excel文件列信息
    /// </summary>
    /// <typeparam name="TK"></typeparam>

    public class ExcelColum<TK>
    {
        public ExcelColum()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// 列名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 列宽度
        /// </summary>
        public short Width { get; set; }


        /// <summary>
        /// 实际Excel类型
        /// </summary>
        internal ExcelColumType RealColumType { get; set; }

        /// <summary>
        /// 列数据类型
        /// </summary>
        public ExcelColumType ColumType { get; set; }

        /// <summary>
        /// 是否对该列求和
        /// </summary>
        public bool DoColumSum
        {
            get;
            set;
        }

        /// <summary>
        /// 计算列单元格结果
        /// </summary>
        public Func<TK, object> ResultFunc { get; set; }

        /// <summary>
        /// 自定特定字段 ， 服务于 ResultByFieldName
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 指定特定字段运行委托,基于FieldName；如果 Result为null 则执行
        /// K 对象
        /// string 字段名
        /// object 返回值
        /// </summary>
        public Func<TK, string, object> ResultByFieldName { get; set; }

    }
}
