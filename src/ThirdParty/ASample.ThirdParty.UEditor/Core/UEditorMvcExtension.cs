using ASample.ThirdParty.UEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ASample.ThirdParty.UEditor.Core
{
    public static class UEditorMvcExtension
    {
        public static void AddUEditorService(string configFileRelativePath = "ueditor.json", bool isCacheConfig = true, string basePath = "", string environmentName = "")
        {
            Config.ConfigFile = configFileRelativePath;
            Config.NoCache = isCacheConfig;
            if (!basePath.IsNullOrWhiteSpace())
            {
                Config.WebRootPath = basePath;
            }
            else
            {
                Config.WebRootPath = HttpContext.Current.Server.MapPath("~/");
            }

            Config.EnvName = environmentName;
        }
    }
}
