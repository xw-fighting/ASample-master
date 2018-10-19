using ASample.ThirdParty.WeChat;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASample.WebSite.Controllers
{
    public class WechatCreateMenuController : Controller
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> CreateMenu()
        {
            //获取菜单
            var menu = GetMenu();
            var accessToken = await WeChatAuthManager.Current.GetAccessTokenAsync();

            //创建菜单
            var createMenuUrl = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={accessToken}";
            var result = await WechatCreateMenuService.PostRequestAsync(createMenuUrl, menu);
            if (result.ErrorCode == "0")
                return result.ErrorMsg;
            return null;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetMenu()
        {
            string menu;
            using (var fs = new FileStream(Server.MapPath("../config") + "\\menuJson.json", FileMode.Open))
            {
                using (var sr = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
                {
                    menu = sr.ReadToEnd();
                }
            }
            return menu;
        }
    }
}
