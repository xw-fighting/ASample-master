using ASample.WebSite.Models.WeCaht;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASample.WebSite.Controllers
{
    public class WeChatOAuthController : Controller
    {
        // GET: WeChatOAuth
        public ActionResult Index()
        {
            return View();
        }

        private HttpClient httpClient;
        private const string appId = "wx5fd56846a94a3375";
        private const string appSecreat = "625e809525b77d3b3fb9d415fdab190a";
        private const string return_url = "http://djwyd8.natappfree.cc/WeChatOAuth/SignInReturn";


        public WeChatOAuthController()
        {
            httpClient = new HttpClient();
        }

        [HttpGet]
        public ActionResult SignInWechat()
        {
            var authorizeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize";
            string authorizationEndpoint = $"{authorizeUrl}?appid={appId}&redirect_uri={return_url}&response_type=code&scope=snsapi_userinfo&state=testOAuth#wechat_redirect";
            return Redirect(authorizationEndpoint);
        }

        public async Task<ActionResult> SignInReturn(string code,string state)
        {
            //获取accessToken
            var accessTokenResult = await GetAccessToken(code);
            //获取用户信息
            var userStr = await GetUserInfo(accessTokenResult.AccessToken, accessTokenResult.Openid);

            return Content("access_token:" + accessTokenResult.AccessToken + "\n expire_in:" 
                + accessTokenResult.ExpiresIn + "\n openId:" + accessTokenResult.Openid + "\t userInfo:" + userStr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization_code"></param>
        /// <returns></returns>
        public async Task<AccessOAuthTokenResult> GetAccessToken(string code)
        {
            var access_tokenUrl = $"https://api.weixin.qq.com/sns/oauth2/access_token";
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
               {
                   {"client_id",appId },
                   {"client_secret", appSecreat},
                   {"grant_type", "authorization_code"},
                   {"code", code}
               });
            var responseContent = await httpClient.PostAsync(access_tokenUrl, content);

            var responseStr = await responseContent.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccessOAuthTokenResult>(responseStr);

            return result;
        }


        public async Task<string> GetUserInfo(string token, string openId)
        {
            var userinfo_url = $"https://api.weixin.qq.com/sns/userinfo";
            var userInfoUrl = userinfo_url + "?access_token=" + token + "&openid=" + openId+"&lang=zh_CN";
            var responseContent = await httpClient.GetAsync(userInfoUrl);
            var responseStr = await responseContent.Content.ReadAsStringAsync();

            return responseStr;
        }
    }
}