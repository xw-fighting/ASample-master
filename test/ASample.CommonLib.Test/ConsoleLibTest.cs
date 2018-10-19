using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.CommonLib.Test
{
    [TestClass]
    public class ConsoleLibTest
    {
        [TestMethod]
        public void GetCurrentPathTest()
        {
            //获取应用程序的当前工作目录。
            var path1 = Directory.GetCurrentDirectory();
            //输出：D:\Project_self\ASample\test\ASample.CommonLib.Test\bin\Debug
            Console.WriteLine(path1);

            //获取关联进程的主模块
            var path2 = Process.GetCurrentProcess().MainModule.FileName;
            //
            Console.WriteLine(path2);

            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径
            string path3 = Environment.CurrentDirectory;
            //输出：D:\Project_self\ASample\test\ASample.CommonLib.Test\bin\Debug
            Console.WriteLine(path3);


            string path4 = AppDomain.CurrentDomain.BaseDirectory;//获取基目录，它由程序集冲突解决程序用来探测程序集。
            Console.WriteLine(path4);

            //string str5 = Application.StartupPath;//获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。
            //string str6 = Application.ExecutablePath;//获取启动了应用程序的可执行文件的路径，包括可执行文件的名称。
            string path7 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//获取或设置包含该应用程序的目录的名称。
            Console.WriteLine(path7);
        }
    }
}
