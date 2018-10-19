using NPOI.HSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Npoi
{
    public class ExcelWriter2
    {
        public static string Write(Dictionary<string,string> cellHeard, IList dataList, string sheetName,string filePath)
        {
            try
            {
                var workBook = new  HSSFWorkbook(); // 工作簿
                var sheet = workBook.CreateSheet(sheetName);
                var row = sheet.CreateRow(0);
                var keys = cellHeard.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    var column = keys[i];
                    row.CreateCell(i).SetCellValue(cellHeard[column]);
                }

                //List对象的值赋值到Excel的单元格里
                int rowIndex = 1;
                foreach (var entity in dataList)
                {
                    var rowTmp = sheet.CreateRow(rowIndex);

                    for (int i = 0; i < keys.Count; i++)
                    {
                        var cellValue = ""; // 单元格的值
                        object propertyValue = null; // 属性的值
                        PropertyInfo propertyInfo = null;
                        var columnHead = keys[i];
                        if (columnHead.IndexOf(".")>=0){
                            // 3.1.1 解析子类属性(这里只解析1层子类，多层子类未处理)
                            string[] propertyArray = keys[i].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            string subClassName = propertyArray[0]; // '.'前面的为子类的名称
                            string subClassProperotyName = propertyArray[1]; // '.'后面的为子类的属性名称
                            System.Reflection.PropertyInfo subClassInfo = entity.GetType().GetProperty(subClassName); // 获取子类的类型
                            if (subClassInfo != null)
                            {
                                // 3.1.2 获取子类的实例
                                var subClassEn = entity.GetType().GetProperty(subClassName).GetValue(entity, null);
                                // 3.1.3 根据属性名称获取子类里的属性类型
                                propertyInfo = subClassInfo.PropertyType.GetProperty(subClassProperotyName);
                                if (propertyInfo != null)
                                {
                                    propertyValue = propertyInfo.GetValue(subClassEn, null); // 获取子类属性的值
                                }
                            }
                        }
                        else
                        {
                            propertyInfo = entity.GetType().GetProperty(columnHead);
                            if (propertyInfo != null)
                            {
                                propertyValue = propertyInfo.GetValue(entity, null);
                            }
                        }

                        if (propertyValue != null)
                        {
                            cellValue = propertyValue.ToString();
                            // 3.3.1 对时间初始值赋值为空
                            if (cellValue.Trim() == "0001/1/1 0:00:00" || cellValue.Trim() == "0001/1/1 23:59:59")
                            {
                                cellValue = "";
                            }
                        }

                        // 填充到Excel的单元格里
                        rowTmp.CreateCell(i).SetCellValue(cellValue);
                    }
                    rowIndex++;
                }
                // 4.生成文件
                FileStream file = new FileStream(filePath, FileMode.Create);
                workBook.Write(file);
                file.Close();

                return filePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
