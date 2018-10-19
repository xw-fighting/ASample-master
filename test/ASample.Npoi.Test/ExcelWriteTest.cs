using ASample.Npoi.Test.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ExcelWriteTest
    {
        [TestMethod]
        public void WriteTest()
        {
            var sheet = LoadExcelSheet(AppDomain.CurrentDomain.BaseDirectory + "/test.xls", null);
            var students = ExcelReader.Read<Student>(sheet);
            var newWorkBook = new HSSFWorkbook();
            var newSheet = newWorkBook.CreateSheet("sheet2");
            ExcelWriter.Write<Student>(sheet,students);
            //var path = AppDomain.CurrentDomain.BaseDirectory + "/test.xls";
            using (var fs = new FileStream(Guid.NewGuid().ToString()+".xls", FileMode.OpenOrCreate))
            {
                newWorkBook.Write(fs);
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
            if (!string.IsNullOrEmpty(sheetName))
            {
                return workBook.GetSheet(sheetName);
            }
            return workBook.GetSheetAt(0);
        }

        public static ISheet CreateSheet(Stream stream, string sheetName)
        {
            var workBook = new HSSFWorkbook(stream);
            if (!string.IsNullOrEmpty(sheetName))
            {
                return workBook.CreateSheet(sheetName);
            }
            return workBook.GetSheetAt(0);
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
    }
}
