using ASample.Npoi.Test.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Npoi.Test
{
    [TestClass]
    public class ExcelWrite2Test
    {
        [TestMethod]
        public void WriteTEst()
        {
            var cellHeadDic = new Dictionary<string, string>()
            {
                    { "Name", "姓名" },
                    { "CardNumber", "证件编号" },
                    { "SchoolName", "填报志愿" },
            };
            var path = AppDomain.CurrentDomain.BaseDirectory + "/test.xls";
            var errMsg = new StringBuilder();
            var students = ExcelReader2.Read<Student>(cellHeadDic, path, out errMsg).ToList();
            var path2 = AppDomain.CurrentDomain.BaseDirectory + "/" + Guid.NewGuid().ToString() + ".xls";
            var path3 = ExcelWriter2.Write(cellHeadDic, students, "sheet1", path2);
            
        }
    }
}
