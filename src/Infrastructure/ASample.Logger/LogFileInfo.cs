using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Logger
{
    /// <summary>
    ///文件日志
    /// </summary>
    public class LogFileInfo
    {
        /// <summary>
        /// 记录信息到文件
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="filename">文件名</param>
        /// <param name="filefix">后缀名</param>
        public static void WriteLogInfo(string info, string path)
        {
            //var path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }

            FileStream newfs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(newfs, Encoding.UTF8);
            sw.Write(DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine + info + Environment.NewLine);
            sw.Close();
            newfs.Close();
        }
    }
}
