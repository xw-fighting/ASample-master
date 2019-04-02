using System;
using System.IO;
using System.Text;

namespace ASample.ThirdParty.IBMWebsphereMQ.Core
{
    public class TxtLog
    {
        private static string filebasepath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";

        /// <summary>
        /// 记录信息到文件
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="filename">文件名</param>
        /// <param name="filefix">后缀名</param>
        public static void LogInfo(string info, string filename, string filefix)
        {
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "Logs\\" + filename + filefix;
                FileStream newfs = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(newfs, Encoding.UTF8);
                sw.Write("记录时间:" + dateStr + Environment.NewLine + info + Environment.NewLine);
                sw.Close();
                newfs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
