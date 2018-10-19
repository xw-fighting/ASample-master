using ASample.Npoi.Test.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Npoi.Test
{
    [TestClass]
    public class ExcelReader2Test
    {
        [TestMethod]
        public void ReadTest()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "/test2.xls";
            //var cellHeader
            var errorMsg = new StringBuilder() ;
            var cellHeadDic = new Dictionary<string, string>()
            {
                    { "Name", "姓名" },
                    { "CardNumber", "证件编号" },
                    { "SchoolName", "填报志愿" },
            };
            var list = ExcelReader2.Read<Student>(cellHeadDic, path,out errorMsg);
            var i = 0;
            foreach (var item in list)
            {
                if (i > 2)
                    break;
                var jsonStr = JsonConvert.SerializeObject(item);
                Console.WriteLine(jsonStr);
                i++;
            }
            

        }
    }
}
