using NPOI.SS.UserModel;
using System;

namespace ASample.Npoi.Config
{
    public class CellConfig
    {
        /// <summary>
        /// 单元格索引
        /// </summary>
        public int? CellIndex { get; set; }

        /// <summary>
        /// 映射的目标名称
        /// </summary>
        public string ModelPropertyName { get; set; }

        /// <summary>
        /// 单元格名称
        /// </summary>
        public string CellHeaderName { get; set; }

        /// <summary>
        /// 映射目标类型
        /// </summary>
        public Type ModelPropertyType { get; set; }

        /// <summary>
        /// 读取cell的时候的映射配置
        /// </summary>
        public Func<ICell, object> ReadConvertion { get; set; }

        /// <summary>
        /// 写入cell的映射配置
        /// </summary>
        public Func<object, object> WriteConversion { get; set; }
    }
}
