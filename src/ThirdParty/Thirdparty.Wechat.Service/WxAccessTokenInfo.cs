using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using Thirdparty.Wechat.Service.WxApiConfig;
using DRapid.Utility.Hash;
using DRapid.Utility.Http;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// 维护token
    /// </summary>
    public class WxAccessTokenInfo
    {
        /// <summary>
        /// 缓存交换文件的文件夹的配置key
        /// </summary>
        public const string WxAccessTokenShareDirConfigKey = "WxAccessTokenShareDir";

        /// <summary>
        /// 缓存交换文件名
        /// </summary>
        public const string WxAccessTokenShareFileName= "WxAccessTokenShare";

        //private static string Token { get; set; }

        //private static int ExpireIn { get; set; }

        private static WxAccessToken _currentToken;

        private static DateTime TokenExpireDateTime { get; set; }

        private readonly string _appId;

        private readonly string _secret;

        private readonly string _exchangeDir;

        private static object IoAccessLock = new object();

        public WxAccessTokenInfo(string appId, string secret)
        {
            _appId = appId;
            _secret = secret;
            _exchangeDir = ConfigurationManager.AppSettings[WxAccessTokenShareDirConfigKey];
        }

        public WxAccessTokenInfo(string appId, string secret, string exchangeDir)
        {
            _appId = appId;
            _secret = secret;
            _exchangeDir = exchangeDir;
            if (exchangeDir.IsNullOrWhiteSpace())
                _exchangeDir = ConfigurationManager.AppSettings[WxAccessTokenShareDirConfigKey];
        }

        public string GetToken()
        {
            lock (IoAccessLock)
            {
                WxAccessToken accessToken;
                //尝试获取token或者获取token变动
                var result = TryGetExchangeTokenWhenChanged(_exchangeDir, _currentToken, out accessToken);
                if (!result)
                {
                    accessToken = _currentToken;
                }

                if (accessToken == null || accessToken.ExpireTime < DateTime.Now)
                {
                    accessToken = GetAccessToken();
                    UpdateExchangeFile(accessToken, _exchangeDir);
                }

                _currentToken = accessToken;
            }

            return _currentToken.AccessToken;
        }

        /// <summary>
        /// 向指定的文件夹更新交换信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="exchangeDir"></param>
        private static void UpdateExchangeFile(WxAccessToken token, string exchangeDir)
        {
            if (exchangeDir.IsNullOrWhiteSpace()) return;
            EnsureDir(exchangeDir);
            var jsonStr = JsonConvert.SerializeObject(token);
            var hash = HashToken(token);
            var fileName = $"{WxAccessTokenShareFileName}{DateTime.Now.ToString("yyyyMMddHHmmfff")}{hash}";
            ClearDir(exchangeDir);
            var fileFullName = Path.Combine(exchangeDir, fileName);
            File.WriteAllText(fileFullName, jsonStr);
        }

        /// <summary>
        /// 清理文件夹下所有文件
        /// </summary>
        /// <param name="dir"></param>
        private static void ClearDir(string dir)
        {
            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    //ignored
                }
            }
        }

        /// <summary>
        /// 从交换区提取信息，判断当前的token缓存是否被其他客户端更新
        /// </summary>
        /// <param name="exchangeDir"></param>
        /// <param name="token"></param>
        /// <param name="tokenInfo"></param>
        /// <returns></returns>
        private static bool TryGetExchangeTokenWhenChanged(string exchangeDir, WxAccessToken token, out WxAccessToken tokenInfo)
        {
            tokenInfo = null;
            if (exchangeDir == null) return false;
            EnsureDir(exchangeDir);
            var files = Directory.GetFiles(exchangeDir);
            //使用最近写入的一个文件
            var file = files.OrderBy(i => i).LastOrDefault();
            var hash = HashToken(token);
            if (!file.IsNullOrWhiteSpace())
            {
                // ReSharper disable once PossibleNullReferenceException
                if (file.Contains(WxAccessTokenShareFileName) && (hash == null || !file.EndsWith(hash)))
                {
                    var jsonStr = File.ReadAllText(file);
                    if (!jsonStr.IsNullOrWhiteSpace())
                    {
                        tokenInfo = ParseToken(jsonStr);
                    }
                    return tokenInfo != null;
                }
            }
            return false;
        }

        private static void EnsureDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// 对Token的json字符串做hash
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static string HashToken(WxAccessToken token)
        {
            if (token == null) return null;
            var jsonStr = JsonConvert.SerializeObject(token);
            var hasher = new MD5Hasher();
            return hasher.Hash(jsonStr);
        }

        /// <summary>
        /// 反序列化token
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        private static WxAccessToken ParseToken(string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<WxAccessToken>(jsonStr);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        private WxAccessToken GetAccessToken()
        {
            HttpStatusCode code;
            var result = RequestSender.Get(string.Format(WxBaseApis.AccesstokenUrl, _appId, _secret), out code);
            if (code != HttpStatusCode.OK)
            {
                throw new Exception("AccessToken 获取失败");
            }
            var token = JsonConvert.DeserializeObject<WxAccessToken>(result);
            token.ExpireTime = DateTime.Now.AddSeconds(token.EexpiresIn);
            return token;
        }

        public class WxAccessToken
        {

            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public int EexpiresIn { get; set; }

            public DateTime ExpireTime { get; set; }
        }
    }
}