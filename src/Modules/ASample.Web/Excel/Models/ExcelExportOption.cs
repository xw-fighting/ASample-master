using System.Collections.Generic;

namespace ASample.Web.Excel.Models
{
    /// <summary>
    /// 导出数据的传入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelExportOption<T>
    {


        public ExcelExportOption()
        {
            FileName = "数据导出";
            Title = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Excel标题
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Excel标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Excel描述
        /// </summary>
        public string Description { get; set; }

        public short DefaultRowHeight { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable<T> Source { get; set; }

        /// <summary>
        /// 列配置
        /// </summary>
        public IEnumerable<ExcelColum<T>> Colums { get; set; }

        /// <summary>
        /// 列头分组
        /// </summary>
        public IEnumerable<ExcelColumGroup> ColumGroups { get; set; }
    }
}
