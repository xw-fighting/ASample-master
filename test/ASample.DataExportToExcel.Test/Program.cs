using ASample.DataExportToExcel.Test.Handlers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.DataExportToExcel.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowCmd();
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ImportOnceGuide();
                    break;
                case "2":
                    ExportOnceGuide();
                    break;
                default:
                    break;
            }
        }

        private static void ExportOnceGuide()
        {
            ImportHandler.ExcelExportToData();
        }

        private static void ImportOnceGuide()
        {
            var path = ConfigurationManager.AppSettings["OnceGuideFolderPath"];
            Console.WriteLine(path);
            var url = ImportHandler.DataExportToExcel(path);
            Console.WriteLine(url);
            Console.ReadKey();
        }

        private static void ShowCmd()
        {
            Console.WriteLine("-------------------------输入数字命令--------------------------");
            Console.WriteLine("1：导入(最多跑一次)办事指南数据[导入前删除]");
            Console.WriteLine("2：从Excel文件中导入办事指南数据");
        }


    }
}
