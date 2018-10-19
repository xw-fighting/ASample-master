using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Reflection;
using NPOI.SS.UserModel;

namespace ASample.Npoi
{
    public class ExcelReader2
    {
        /// <summary>
        /// 将Excel中的文件读取出来保存到List集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cellHeard"></param>
        /// <param name="filePath"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static IList<T> Read<T>(Dictionary<string, string> cellHeard, string filePath, out StringBuilder errorMsg) where T:new()
        {
            List<T> enlist = new List<T>();
            errorMsg = new StringBuilder();
            try
            {
                if (Regex.IsMatch(filePath, ".xls$")) // 2003
                    enlist = LoadToList<T>(cellHeard, filePath, out errorMsg);
                else if (Regex.IsMatch(filePath, ".xlsx$")) // 2007
                    throw new Exception("请将excel文件保存为2003版本");
                return enlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<T> LoadToList<T>(Dictionary<string, string> cellHeard, string filePath, out StringBuilder errorMsg) where T: new()
        {
            errorMsg = new StringBuilder();
            var dataList = new List<T>();
            List<string> keys = cellHeard.Keys.ToList(); // 要赋值的实体对象属性名称
            try
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var workBook = new HSSFWorkbook(fileStream);
                    var sheet = workBook.GetSheetAt(0);
                    for (int i = 0; i < sheet.LastRowNum; i++)
                    {
                        if (sheet.GetRow(i) == null)//判断当前行是否为空
                            break;

                        T entity = new T();
                        string errStr = "";

                        for (int j = 0; j < keys.Count; j++)//头部列数
                        {
                            var propertyName = keys[j];
                            //若属性头的名称包含'.',就表示是子类里的属性，那么就要遍历子类，eg：Student.Name
                            if (propertyName.IndexOf(".") >= 0)
                            {

                            }
                            else
                            {
                                //通过名称获取类的属性
                                var propertyInfo = entity.GetType().GetProperty(propertyName);
                                if(propertyInfo != null)
                                {
                                    try
                                    {
                                        // Excel单元格的值转换为对象属性的值，若类型不对，记录出错信息
                                        propertyInfo.SetValue(entity, GetExcelCellToProperty(propertyInfo.PropertyType, sheet.GetRow(i).GetCell(j)), null);
                                    }
                                    catch (Exception e)
                                    {
                                        if (errStr.Length == 0)
                                        {
                                            errStr = "第" + i + "行数据转换异常：";
                                        }
                                        errStr += cellHeard[keys[j]] + "列；";
                                    }
                                }
                            }
                        }
                        // 若有错误信息，就添加到错误信息里
                        if (errStr.Length > 0)
                        {
                            errorMsg.AppendLine(errStr);
                        }
                        dataList.Add(entity);
                    }
                    return dataList;
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取Excel中的单元格中的值赋值到对象中
        /// </summary>
        /// <param name="targetType">接收Excel单元格中的对象</param>
        /// <param name="cell">Excel中单元格</param>
        /// <returns></returns>
        private static object GetExcelCellToProperty(Type targetType, ICell cell)
        {
            var rs = targetType.IsValueType ? Activator.CreateInstance(targetType) : null;

            // 1.判断传递的单元格是否为空
            if (cell == null || string.IsNullOrEmpty(cell.ToString()))
            {
                return rs;
            }

            // 2.Excel文本和数字单元格转换，在Excel里文本和数字是不能进行转换，所以这里预先存值
            object sourceValue = null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    break;

                case CellType.Boolean:
                    break;

                case CellType.Error:
                    break;

                case CellType.Formula:
                    break;

                case CellType.Numeric:
                    sourceValue = cell.NumericCellValue;
                    break;

                case CellType.String:
                    sourceValue = cell.StringCellValue;
                    break;

                case CellType.Unknown:
                    break;

                default:
                    break;
            }

            string valueDataType = targetType.Name;

            // 在这里进行特定类型的处理
            switch (valueDataType.ToLower()) // 以防出错，全部小写
            {
                case "string":
                    rs = sourceValue.ToString();
                    break;
                case "int":
                case "int16":
                case "int32":
                    rs = (int)Convert.ChangeType(cell.NumericCellValue.ToString(), targetType);
                    break;
                case "float":
                case "single":
                    rs = (float)Convert.ChangeType(cell.NumericCellValue.ToString(), targetType);
                    break;
                case "datetime":
                    rs = cell.DateCellValue;
                    break;
                case "guid":
                    rs = (Guid)Convert.ChangeType(cell.NumericCellValue.ToString(), targetType);
                    return rs;
            }
            return rs;
        }
    }
}
