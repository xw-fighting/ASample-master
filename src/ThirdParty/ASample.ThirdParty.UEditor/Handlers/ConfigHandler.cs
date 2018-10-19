using ASample.ThirdParty.UEditor.Models;
using Newtonsoft.Json.Linq;

namespace ASample.ThirdParty.UEditor.Handlers
{
    public class ConfigHandler
    {
        /// <summary>
        /// 读取ueditor.json文件内容
        /// </summary>
        /// <returns></returns>
        public JObject Process()
        {
            return Config.Items;
        }
    }
}
