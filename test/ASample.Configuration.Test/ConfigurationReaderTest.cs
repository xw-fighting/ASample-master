using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Configuration.Test
{
    [TestClass]
    public class ConfigurationReaderTest
    {
        [TestMethod]
        public void Test()
        {
            string str1 = Process.GetCurrentProcess().MainModule.FileName;    //可获得当前执行的exe的文件名。  
            string str2 = Environment.CurrentDirectory;          //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            string str3 = Directory.GetCurrentDirectory(); //获取应用程序的当前工作目录。
            string str4 = AppDomain.CurrentDomain.BaseDirectory;//获取基目录，它由程序集冲突解决程序用来探测程序集。
            //string str5 = Application.StartupPath;                                   //获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。（如：D:/project/集团客户短信服务端/bin/Debug）
            //string str6 = Application.ExecutablePath;         //获取启动了应用程序的可执行文件的路径，包括可执行文件的名称。
            string str7 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;  //获取或设置包含该应用程序的目录的名称。
            Console.WriteLine(str1);
        }

        [TestMethod]
        public void ReadFileTest()
        {
            string filePath = Directory.GetCurrentDirectory(); //获取应用程序的当前工作目录。
            //c#文件流读文件 
            using (FileStream fsRead = new FileStream(filePath + @"\config\menuJson.json", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                Console.WriteLine(myStr);
            }
        }

        [TestMethod]
        public void ConfigurationReadTest()
        {
            string filePath = Directory.GetCurrentDirectory();
            var jsonStr = ConfigurationReader.Read("menuJson");
            Console.WriteLine(jsonStr);
        }
    }
}
