using ASample.Serialize.XmlSerialize;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

namespace ASample.Configuration
{
    /// <summary>
    /// 读取xml文件里的信息
    /// </summary>
    public static class ConfigurationReader
    {
        static ConfigurationReader()
        {
            Look(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config"));
        }

        public static void Look(string absluteFolderPath)
        {
            if (!Directory.Exists(absluteFolderPath))
            {
                Directory.CreateDirectory(absluteFolderPath);
            }
            _configurationDir = absluteFolderPath;
        }

        /// <summary>
        /// 这里默认使用应用程序域的目录下面的config文件夹，作为配置文件的文件夹
        /// </summary>
        private static string _configurationDir;

        private static ConcurrentDictionary<Type, object> list = new ConcurrentDictionary<Type, object>();

        public static T Read<T>(IDeserializer deserializer) where T : class
        {
            return list.GetOrAdd(typeof(T), key =>
            {
                string filePath;
                var content = ReadFileText(typeof(T).Name, out filePath);
                var result = deserializer.Deserialize<T>(content);
                //StartMonitor<T>(filePath);
                return result;
            }) as T;
        }

        public static string Read<T>()
        {
            var filePath = string.Empty;
            var content = ReadFileText(typeof(T).Name, out filePath);
            return content;
        }

        /// <summary>
        /// 在项目文件夹config 下读取指定Json文件名的内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Read(string fileName)
        {
            var filePath = string.Empty;
            var content = ReadFileText(fileName, out filePath);
            return content;
        }

        private static string ReadFileText(string fileName, out string filePath)
        {
            //与扩展名无关
            var files = Directory.EnumerateFiles(_configurationDir, fileName + ".*").ToArray();
            if (files.Count() != 1)
            {
                throw new Exception(string.Format("在{0}中找到了0个或者多个配置项：" + fileName + ".*", _configurationDir));
            }
            filePath = files[0];
            return File.ReadAllText(filePath);
        }
        ///监视文件变化
        //private static void StartMonitor<T>(string filePath)
        //{
        //    var dep = new FileCacheDependency(filePath);
        //    object value;
        //    dep.OnDependencyChanged += () =>
        //    {
        //        Cache.TryRemove(typeof(T), out value);
        //    };

        //    FileDependencyMonitor.StartMonitor(dep);
        //}
    }
}
