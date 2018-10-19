using ASample.ExtensionMethod;
using ASample.Npoi.Config;
using ASample.Npoi.Core;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace ASample.Npoi
{
    public class ExcelReader
    {
        public static IList<T> Read<T>(ISheet sheet, int startRowIndex = 0,
           int startCellIndex = 0, int endCellIndex = 0, bool firstRowAsHeader = true)
           where T : class, new()
        {
            var config = ExcelTableMap.Config<T>(startRowIndex, startCellIndex, endCellIndex, firstRowAsHeader);
            return Read<T>(sheet, config);
        }

        public static IList<T> Read<T>(ISheet sheet, TableConfig config)
            where T : class, new()
        {
            var result = new List<T>();
            var firstRowIndex = config.StartRowIndex;
            int failureCount = 0;
            for (var index = 0; index < int.MaxValue; index++)
            {
                var tItem = new T();
                var row = sheet.GetRow(index);
                if (index < config.StartRowIndex)
                {
                    continue;
                }
                //使用一个谓词来判断是否继续提取数据，如果是false则代表终止
                if (config.EnumerateCondition(row, failureCount) == false)
                {
                    break;
                }
                //使用一个谓词来判断是否继续提取数据，如果是null代表虽然钚提取数据，但是继续下一行
                if (config.EnumerateCondition(row, failureCount) == null)
                {
                    failureCount++;
                    firstRowIndex++;
                    continue;
                }
                if (index == firstRowIndex && config.FirstRowAsHeader)
                {
                    FillCellConfigs(row, config);
                    continue;
                }
                failureCount = 0;
                foreach (var cellConfig in config.CellConfigs)
                {
                    if (cellConfig.CellIndex == null)
                    {
                        continue;
                    }
                    var cell = row.GetCell(cellConfig.CellIndex.Value);
                    if (cell == null)
                    {
                        continue;
                    }
                    LoadValueFromCell(cell, cellConfig, tItem);
                }
                result.Add(tItem);
            }
            return result;
        }

        private static void FillCellConfigs(IRow headerRow, TableConfig config)
        {
            //var length = headerRow.Cells.Count;
            var propertyCount = config.MapModelType.GetProperties().Length;
            int? trackedStartIndex = null;
            for (var index = 0; index < int.MaxValue; index++)

            {
                if (index < config.StartRowIndex)
                    continue;
                if (trackedStartIndex != null)
                {
                    var endCellIndex2 = propertyCount + trackedStartIndex.Value;
                    if (index > Math.Max(endCellIndex2, config.EndCellIndex))
                        break;
                }
                var cell = headerRow.GetCell(index);
                if (cell == null)
                    continue;
                trackedStartIndex = trackedStartIndex ?? index;
                var cellHeaderName = cell.StringCellValue;
                config.AddCellConfigOrUpdateConfigCellIndex(cellHeaderName, index);
            }
        }

        private static void LoadValueFromCell(ICell cell, CellConfig config, object target)
        {
            var fieldValue = config.ReadConvertion != null
               ? config.ReadConvertion(cell)
               : cell.Extract(config.ModelPropertyType);
            PropertyFieldSetter.Instance.Set(target, config.ModelPropertyName, fieldValue);
        }
    }
}
