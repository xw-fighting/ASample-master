using ASample.ThirdParty.WeChat.WeChatCreateMenu.Model.OutputResult;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;


namespace ASample.ThirdParty.WeChat
{
    public static class WechatCreateMenuService
    {
        /// <summary>
        /// 创建菜单按钮
        /// </summary>
        /// <param name="menuJsonStr"></param>
        /// <returns></returns>
        public static async Task<string> CreateMenuAsync(string menuJsonStr)
        {
            //获取微信令牌
            var accessToken = await WeChatAuthManager.Current.GetAccessTokenAsync();

            //创建菜单
            var createMenuUrl = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={accessToken}";
            var result = await WechatCreateMenuService.PostRequestAsync(createMenuUrl, menuJsonStr);
            if (result.ErrorCode == "0")
                return result.ErrorMsg;
            return null;
        }
        /// <summary>
        /// post方法访问
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static async Task<CreateMenuResult> PostRequestAsync(string postUrl, string postData)
        {
            // 设置参数
            var httpClient = new HttpClient();
            var content = new StringContent(postData);
            var response = await httpClient.PostAsync(postUrl, content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateMenuResult>(jsonResult);
            return result;
        }
    }
}
