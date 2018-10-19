using ASample.Npoi.Config;
using ASample.Npoi.Core;
using ASample.Npoi.Test.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Npoi.Test
{
    [TestClass]
    public class ExcelReaderTest
    {
        [TestMethod]
        public void ReadExcel()
        {
            var sheet = LoadExcelSheet(AppDomain.CurrentDomain.BaseDirectory + "/test.xls", null);

            var students = ExcelReader.Read<Student>(sheet);
            //输出测试结果
            var firstStudent = students.FirstOrDefault();
            if (firstStudent != null)
            {
                var content = JsonConvert.SerializeObject(firstStudent);
                Console.WriteLine(content);
            }
        }

        [TestMethod]
        public void ReadExcel2()
        {
            var sheet = LoadExcelSheet(AppDomain.CurrentDomain.BaseDirectory + "/test2.xls", null);
            //ExcelTableMap.Config<Student>(12, 8, 9)
            //   .Map(i => i.CardNumber, "证件编号")
            //   .Map(i => i.Name, "名字")
            //   .Map(i => i.SchoolName, "填报志愿");
            var students = ExcelReader.Read<Student>(sheet);
            //输出测试结果
            var firstStudent = students.FirstOrDefault();
            if (firstStudent != null)
            {
                var content = JsonConvert.SerializeObject(firstStudent);
                Console.WriteLine(content);
            }
        }

        /// <summary>
        /// 重载，根据一个文件路径来加载Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static ISheet LoadExcelSheet(string filePath, string sheetName)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return LoadExcelSheet(fs, sheetName);
            }
        }

        /// <summary>  
        /// 根据名称加载sheet
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="sheetName">工作表sheet的名称，如果为空，则加载第一个sheet</param>
        /// <returns></returns>
        public static ISheet LoadExcelSheet(Stream stream, string sheetName)
        {
            var workBook = new HSSFWorkbook(stream);
            if (!string.IsNullOrWhiteSpace(sheetName))
            {
                return workBook.GetSheet(sheetName);
            }
            return workBook.GetSheetAt(0);
        }

        public static ISheet CreateSheet(Stream stream, string sheetName)
        {
            var workBook = new HSSFWorkbook(stream);
            if (!string.IsNullOrWhiteSpace(sheetName))
            {
                return workBook.CreateSheet(sheetName);
            }
            return workBook.GetSheetAt(0);
        }

    }

    
}
