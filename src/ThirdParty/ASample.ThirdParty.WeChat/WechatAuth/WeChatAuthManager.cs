using ASample.Configuration;
using ASample.Serialize.XmlSerialize;
using ASample.ThirdParty.WeChat.Model;
using ASample.ThirdParty.WeChat.WechatAuth.OutResult;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace ASample.ThirdParty.WeChat
{
    /// <summary>
    /// 获取微信授权登录管理
    /// </summary>
    public class WeChatAuthManager
    {
        private static WeChatAuthManager _current;

        private readonly MemoryCache _cache;

        public static WeChatAuthManager Current
        {
            get
            {
                if (_current == null)
                    _current = new WeChatAuthManager();
                return _current;
            }
        }

        public WeChatAuthManager()
        {
            _cache = MemoryCache.Default;
        }

        private readonly string _wechatKey = "wechat_accesstoken";
        /// <summary>
        /// 获取微信accesstoken, 优先从缓存获取，其次重新请求获取
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync()
        {
            var accessToken = _cache.Get(_wechatKey) as string;
            if (!string.IsNullOrWhiteSpace(accessToken))
                return accessToken;

            var result = await RequestAccessTokenAsync();
            _cache.Remove(_wechatKey);
            if (string.IsNullOrWhiteSpace(result.AccessToken))
                return result.AccessToken;
            _cache.Add(new CacheItem(_wechatKey, result.AccessToken), new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(result.ExpireTime - 10) }); //在过期时间(秒) 减去10秒，冗余
            return result.AccessToken;
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<WxUserInfoResult> GetBasicInfoAsync(string openId, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                accessToken = await GetAccessTokenAsync();
            var url = $"https://api.weixin.qq.com/cgi-bin/user/info?access_token={accessToken}&openid={openId}&lang=zh_CN";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var respStr = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WxUserInfoResult>(respStr);
            return result;
        }

        /// <summary>
        /// 请求微信服务器，以获取accesstoken信息
        /// 文档地址 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140183
        /// </summary>
        /// <returns></returns>
        private async Task<AccessTokenResult> RequestAccessTokenAsync()
        {
            var config = ConfigurationReader.Read<WeChatConfig>(new XmlSerialize());
            var wxAppId = config.WxAppid;
            var wxSecert = config.WxSecret;
            var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={wxAppId}&secret={wxSecert}";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, null);
            var respStr = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AccessTokenResult>(respStr);
            return result;
        }

        public void Dispose()
        {
            _cache?.Dispose();
        }
    }
}
