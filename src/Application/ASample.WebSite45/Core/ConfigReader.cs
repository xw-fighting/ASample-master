using System.Configuration;

namespace ASample.WebSite45.Core
{
    public class ConfigReader
    {
        public static string ReadByKey(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            return value;
        }
    }
}