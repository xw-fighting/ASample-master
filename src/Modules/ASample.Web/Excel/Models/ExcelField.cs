using ASample.Web.Excel.Values;
using System;

namespace ASample.Web.Excel.Models
{
    /// <summary>
    /// Excel文件字段信息
    /// </summary>
    public class ExcelField
    {
        public static ExcelField Default = new ExcelField() { Name = String.Empty, Type = ExcelColumType.String, Value = null };
        public static ExcelField ValidOfDefault(ExcelField value)
        {
            if (value == null)
                return Default;
            return value;
        }
        public ExcelField(string name, ExcelColumType type)
        {
            Name = name;
            Type = type;
        }
        public ExcelField()
        { }
        public string Name { get; set; }
        public ExcelColumType Type { get; set; }
        public object Value { get; set; }
    }
}
