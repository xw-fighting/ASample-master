using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASample.Npoi.Config
{
    public class TableConfig
    {
        public TableConfig()
        {
            EnumerateCondition = (row, failureCheckCount) =>
            {
                if (failureCheckCount > 5)
                    return false;
                if (row == null)
                    return null;
                if (row.Cells.Count == 0)
                    return null;
                return true;
            };
            CellConfigs = new List<CellConfig>();
        }

        /// <summary>
        /// 是否将第一行当作列头
        /// </summary>
        public bool FirstRowAsHeader { get; set; }

        /// <summary>
        /// 表示目标Sheet是否使用了模版
        /// </summary>
        public bool IsSheetFromTemplate { get; set; }

        /// <summary>
        /// 导出Excel的时候自动配置
        /// </summary>
        public bool AutoWrite { get; set; }

        /// <summary>
        /// 开始行的索引
        /// </summary>
        public int StartRowIndex { get; set; }

        /// <summary>
        /// 开始的单元格索引
        /// </summary>
        public int StartCellIndex { get; set; }

        /// <summary>
        /// 结束的单元格索引
        /// </summary>
        public int EndCellIndex { get; set; }

        /// <summary>
        /// 映射的模型信息
        /// </summary>
        public Type MapModelType { get; set; }

        /// <summary>
        /// 读取条件，如果不满足，则不继续循环提取行
        /// </summary>
        public Func<IRow, int, bool?> EnumerateCondition { get; set; }

        /// <summary>
        /// 所有的列配置
        /// </summary>
        public List<CellConfig> CellConfigs { get; set; }

        /// <summary>
        /// 根据单元格的列头名称获取单元格配置
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        public CellConfig GetCellConfig(string cellName)
        {
            return CellConfigs.FirstOrDefault(i => i.CellHeaderName == cellName);
        }

        /// <summary>
        /// 根据属性名称获取配置
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public CellConfig GetCellConfigByProperty(string propertyName)
        {
            return CellConfigs.FirstOrDefault(i => i.ModelPropertyName == propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public CellConfig GetCellConfig(int cellIndex)
        {
            return CellConfigs.FirstOrDefault(i => i.CellIndex == cellIndex);
        }

        /// <summary>
        /// 添加一个列配置
        /// </summary>
        /// <param name="cellConfig"></param>
        public void AddCellConfig(CellConfig cellConfig)
        {
            if (GetCellConfig(cellConfig.ModelPropertyName) != null)
            {
                throw new InvalidOperationException($"为列{cellConfig.CellHeaderName}重复指定了映射配置");
            }
            CellConfigs.Add(cellConfig);
        }

        /// <summary>
        /// 添加一个列配置
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="cellIndex"></param>
        /// <param name="throwIfUnmatch"></param>
        public void AddCellConfigOrUpdateConfigCellIndex(string cellName, int cellIndex, bool throwIfUnmatch = false)
        {
            //var cellConfig = GetCellConfig(cellIndex);
            //if (cellConfig != null)
            //{
            //    //这里更新列配置的索引信息
            //    cellConfig.CellIndex = cellIndex;
            //    return;
            //}
            var cellConfig = GetCellConfig(cellName);
            if (cellConfig != null)
            {
                cellConfig.CellIndex = cellIndex;
                return;
            }
            //这里添加新的列配置
            var property = MapModelType.GetProperty(cellName);
            if (property == null && throwIfUnmatch)
                throw new InvalidOperationException($"在指定的类型{MapModelType.FullName}上找不到名为{cellName}的属性");
            if (property == null)
                return;
            cellConfig = new CellConfig
            {
                CellIndex = cellIndex,
                ModelPropertyType = property.PropertyType,
                ModelPropertyName = cellName,
                CellHeaderName = cellName,
            };
            CellConfigs.Add(cellConfig);
        }
    }

    public class TableConfig<T> : TableConfig
    {
        public TableConfig()
        {
            MapModelType = typeof(T);
        }
    }
}
