using ASample.WebSite.Models.WeiboOAuth;
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
    public class WeiboOAuthController : Controller
    {
        private HttpClient httpClient;
        private const string appKey = "1588412454";
        private const string appSecreat = "259a80d6872f0697f3270a49f698e9fe";
        private const string return_url = "http://30b04b4c.ngrok.io/WeiboOAuth/SignInReturn";
        
        
        public WeiboOAuthController()
        {
            httpClient = new HttpClient();
        }
        // GET: WeiboOAuth
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SignInWeibo()
        {
            var authorizeUrl = "https://api.weibo.com/oauth2/authorize";
            string authorizationEndpoint = $"{authorizeUrl}?client_id={appKey}&redirect_uri={return_url}";
            return Redirect(authorizationEndpoint);
        }

        public async Task<ActionResult> SignInReturn(string code)
        {
            //获取accessToken
            var accessTokenResult = await GetAccessToken(code);
            //获取用户信息
            var userStr = await GetUserInfo(accessTokenResult.AccessToken, accessTokenResult.UId);

            return Content("access_token:"+accessTokenResult.AccessToken+"\n expire_in:"+accessTokenResult.ExpiresIn+ "\n remind_in"+accessTokenResult.RemindIn+"\t userInfo:"+ userStr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization_code"></param>
        /// <returns></returns>
        public async Task<AccessTokenResult> GetAccessToken(string authorization_code)
        {
            var access_tokenUrl = "https://api.weibo.com/oauth2/access_token";
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
               {
                   {"client_id",appKey },
                   {"client_secret", appSecreat},
                   {"grant_type", "authorization_code"},
                   {"code", authorization_code},
                   {"redirect_uri", return_url},
               });
            var responseContent = await httpClient.PostAsync(access_tokenUrl, content);

            var responseStr = await responseContent.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccessTokenResult>(responseStr);

            return result;
        }

        
        public async Task<string> GetUserInfo(string token,string uid)
        {
            var userinfo_url = "https://api.weibo.com/2/users/show.json";
            var userInfoUrl = userinfo_url + "?access_token=" + token+"&uid="+uid;
            var responseContent = await httpClient.GetAsync(userInfoUrl);
            var responseStr = await responseContent.Content.ReadAsStringAsync();

            return responseStr;
        }
    }

}