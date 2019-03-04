using System;
using System.IO;
using System.Text;

namespace ASample.ThirdParty.WeChat.WeChatPay.Core
{
    public class LogHelper
    {
        public static void WriteLogs(string info)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
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

        /// <summary>
        /// 追加内容到文件
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="filename">文件名</param>
        /// <param name="filefix">后缀名</param>
        public static void AppendToFile(string info, string filename, string filefix = ".txt")
        {
            try
            {
                var _filebasepath = AppDomain.CurrentDomain.BaseDirectory;
                var filePath = _filebasepath + "\\" + filename + filefix;

                if (!System.IO.Directory.Exists(_filebasepath)) Directory.CreateDirectory(_filebasepath);

                if (!File.Exists(filePath)) File.Create(filePath).Close();

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.Write(info);
                    sw.Close();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 覆盖文本内容
        /// </summary>
        /// <param name="info"></param>
        /// <param name="filename"></param>
        /// <param name="filefix"></param>
        public static void CoverToFile(string info, string filename, string filefix = ".txt")
        {
            try
            {
                var _filebasepath = AppDomain.CurrentDomain.BaseDirectory;
                var filePath = _filebasepath + "\\" + filename + filefix;

                if (!System.IO.Directory.Exists(_filebasepath)) Directory.CreateDirectory(_filebasepath);

                if (!File.Exists(filePath)) File.Create(filePath).Close();

                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    sw.Write(info);
                    sw.Close();
                }
            }
            catch
            {
            }
        }

        public static string ReadFileText(string fileName, string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName)) return string.Empty;
                if (string.IsNullOrEmpty(filePath)) filePath = AppDomain.CurrentDomain.BaseDirectory;

                return File.ReadAllText(filePath + fileName);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
