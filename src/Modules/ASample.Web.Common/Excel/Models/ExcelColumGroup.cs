
namespace ASample.Web.Common.Excel.Models
{
    /// <summary>
    /// Excel文件组信息
    /// </summary>
    public class ExcelColumGroup
    {
        /// <summary>
        /// 合并单元格列数
        /// </summary>
        public int Cols { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 其实位置索引
        /// </summary>
        public int StartIndex { get; set; }

    }
}
