using Newtonsoft.Json.Linq;
using ASample.ThirdParty.UEditor.NetCore.Models;

namespace ASample.ThirdParty.UEditor.NetCore.Handlers
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class ConfigHandler
    {
        public JObject Process()
        {
            return Config.Items;
        }
    }
}
