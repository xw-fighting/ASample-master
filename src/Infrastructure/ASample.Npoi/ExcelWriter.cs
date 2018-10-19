using ASample.ExtensionMethod;
using ASample.Npoi.Config;
using ASample.Npoi.Core;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Npoi
{
    public class ExcelWriter
    {
        /// <summary>
        /// 将数据写入Excel Sheet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="data"></param>
        /// <param name="isSheetFromTemplate">是否已经包含模板</param>
        /// <param name="subConfigKey"></param>
        /// <param name="autoWrite"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="startCellIndex"></param>
        /// <param name="endCellIndex"></param>
        public static void Write<T>(ISheet sheet, IEnumerable<T> data, bool isSheetFromTemplate = false,
            string subConfigKey = null, bool autoWrite = false, int startRowIndex = 0, int startCellIndex = 0,
            int endCellIndex = 0)
        {
            var config = ExcelTableMap.Config<T>(subConfigKey, startRowIndex, startCellIndex, endCellIndex);
            config.AutoWrite = autoWrite;
            config.FirstRowAsHeader = isSheetFromTemplate;
            config.IsSheetFromTemplate = isSheetFromTemplate;
            Write(sheet, data, config);
        }

        public static ISheet CreateAndWriteToSheet<T>(string sheetName, IEnumerable<T> data,
            bool isSheetFromTemplate = false, string subConfigKey = null, bool autoWrite = false,
            int startRowIndex = 0, int startCellIndex = 0, int endCellIndex = 0)
        {
            var workBook = new HSSFWorkbook();
            var sheet = workBook.GetSheet(sheetName);
            if (sheet == null)
                sheet = workBook.CreateSheet(sheetName);
            Write(sheet, data, isSheetFromTemplate, subConfigKey, autoWrite, startRowIndex, startCellIndex, endCellIndex);
            return sheet;
        }

        public static Stream CreateAndWriteToStream<T>(string sheetName, IEnumerable<T> data,
           bool isSheetFromTemplate = false, string subConfigKey = null, bool autoWrite = false,
           int startRowIndex = 0, int startCellIndex = 0, int endCellIndex = 0)
        {
            var sheet = CreateAndWriteToSheet(sheetName, data, isSheetFromTemplate, subConfigKey, autoWrite,
                startRowIndex, startCellIndex, endCellIndex);
            return sheet.Workbook.WriteToMemoryStream();
        }

        public static void Write<T>(ISheet sheet, IEnumerable<T> data, TableConfig config)
        {
            var rowIndex = config.StartRowIndex;
            if (!config.IsSheetFromTemplate)
            {
                //不使用模版进行导出
                var cellKeys = config.CellConfigs
                    .OrderBy(i => i.CellIndex)
                    .Select(i => i.ModelPropertyName).ToList();
                var sortKeys = cellKeys.ToList();
                var configedKeys = typeof(T).GetProperties().Select(i => i.Name).ToList();
                if (config.AutoWrite)
                    cellKeys = cellKeys.Union(configedKeys).ToList();
                cellKeys = cellKeys.OrderDepend(sortKeys, (f, s) => f == s).ToList();
                var headerRow = sheet.GetRow(rowIndex);
                if (headerRow == null)
                    headerRow = sheet.CreateRow(rowIndex);
                var cellIndex = config.StartCellIndex;
                var list = new List<string>();
                var headerCellValue = cellKeys.Select(i =>
                //config.GetCellConfigByProperty(i).IfNotNull(c => c.CellHeaderName, i)
                {
                    var cellModel = config.GetCellConfigByProperty(i);
                    if (cellModel != null)
                        return cellModel.CellHeaderName;
                    else
                        return i;
                });

                foreach (var cellKey in headerCellValue)
                {
                    var cell = headerRow.GetCell(cellIndex);
                    if (cell == null)
                        cell = headerRow.CreateCell(cellIndex);
                    UpdateCellWithValue(cell, null, cellKey);
                    cellIndex++;
                }
                rowIndex++;
                foreach (var item in data)
                {
                    var row = sheet.GetRow(rowIndex);
                    if (row == null)
                        row = sheet.CreateRow(rowIndex);
                    WriteRow(cellKeys, CellKeyType.PropertyKey, row, item, config);
                    rowIndex++;
                }
            }
            else
            {
                //使用模版进行导出
                var headerRow = sheet.GetRow(rowIndex);
                var cellHeaders = headerRow.Cells.Where(i => i != null)
                    .Select(i => i.ToString().Trim())
                    .ToList();
                var configedKeys = config.CellConfigs
                    .OrderBy(i => i.CellIndex)
                    .Select(i => i.CellHeaderName.Trim()).ToList();
                cellHeaders = cellHeaders.OrderDepend(configedKeys, (f, s) => f == s).ToList();
                rowIndex++;
                foreach (var item in data)
                {
                    var row = sheet.GetRow(rowIndex);
                    if (row == null)
                        row = sheet.CreateRow(rowIndex);
                    WriteRow(cellHeaders, CellKeyType.CellHeaderKey, row, item, config);
                    rowIndex++;
                }
            }
        }
        private static void WriteRow<T>(List<string> cellKeys, CellKeyType keyType,
           IRow row, T dataItem, TableConfig config)
        {
            var cellIndex = config.StartCellIndex;
            foreach (var cellKey in cellKeys)
            {
                var cellConfig = keyType == CellKeyType.PropertyKey
                    ? config.GetCellConfigByProperty(cellKey)
                    : config.GetCellConfig(cellKey);
                if (cellConfig == null && config.AutoWrite && !config.IsSheetFromTemplate)
                    cellConfig = new CellConfig
                    {
                        CellHeaderName = cellKey,
                        ModelPropertyName = cellKey
                    };
                if (cellConfig != null)
                {
                    var cell = row.GetCell(cellIndex);
                    if (cell == null)
                        cell = row.CreateCell(cellIndex);
                    UpdateCellWithValue(cell, cellConfig, PropertyFieldLoader.Instance.Load(dataItem, cellKey));
                }

                cellIndex++;
            }
        }
        private static void UpdateCellWithValue(ICell cell, CellConfig config, object target)
        {
            var value = target;
            if (config != null && config.WriteConversion != null)
                value = config.WriteConversion(value);
            if (value is string)
            {
                cell.SetCellValue((string)value);
                return;
            }

            Double.TryParse(value.ToString(), out double dValue);
            if (dValue > 0)
            {
                cell.SetCellValue(dValue);
                return;
            }
            if (value is DateTime)
            {
                //cell.SetCellValue((DateTime) value);
                cell.SetCellValue(((DateTime)value).ToString("yyyy/MM/dd HH:mm:ss"));
                return;
            }
            if (value is bool)
            {
                cell.SetCellValue((bool)value);
                return;
            }
            cell.SetCellValue(value.ToString());
        }

        public enum CellKeyType
        {
            CellHeaderKey,
            PropertyKey
        }
    }
}
