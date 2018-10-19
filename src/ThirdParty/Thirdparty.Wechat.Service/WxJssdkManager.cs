using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Thirdparty.Wechat.Service
{
    public class WxJssdkManager
    {
        public WxTicketManager WxTicketManager { get; }

        private WxOption WxOption { get; }

        public WxJssdkManager(WxOption wxOption)
        {
            WxOption = wxOption;

            WxTicketManager = new WxTicketManager(wxOption.AppId, wxOption.Secret);
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            var random = new Random();
            return EncryptToMd5(random.Next(1000).ToString());
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <returns></returns>
        private string CreateSha1(IDictionary parameters)
        {
            var akeys = new ArrayList(parameters.Keys);
            akeys.Sort();
            var paramList = new List<string>();
            akeys.Foreach<string>(k => { paramList.Add($"{k}={(string)parameters[k]}"); });
            return EncryptToSha1(paramList.Join("&")).ToLower();
        }

        /// <summary>
        /// md5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptToMd5(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var str1 = Encoding.UTF8.GetBytes(str);
            var str2 = md5.ComputeHash(str1, 0, str1.Length);
            md5.Clear();
            md5.Dispose();
            return Convert.ToBase64String(str2);
        }

        /// <summary>
        /// sha1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string EncryptToSha1(string str)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成byte[]
            var enc = new ASCIIEncoding();
            var dataToHash = enc.GetBytes(str);
            //Hash运算
            var dataHashed = sha.ComputeHash(dataToHash);
            //将运算结果转换成string
            var hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;
        }

        /// <summary>
        /// 获取JS-SDK权限验证的签名Signature
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="noncestr"></param>
        /// <param name="timestamp"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetSignature(string ticket, string noncestr, string timestamp, string url)
        {
            var parameters = new Hashtable
            {
                {"jsapi_ticket", ticket},
                {"noncestr", noncestr},
                {"timestamp", timestamp},
                {"url", url}
            };

            return CreateSha1(parameters);
        }

        public WxJssdkSignature GetJssdkSignature(string url)
        {
            var timestamp = GetTimestamp();
            var nonceStr = GetNoncestr();
            var jsTicket = WxTicketManager.GetTicket();
            var signature = GetSignature(jsTicket, nonceStr, timestamp, url);

            var jssdkSignature = new WxJssdkSignature
            {
                AppId = WxOption.AppId,
                Timestamp = timestamp,
                NonceStr = nonceStr,
                Signature = signature
            };
            return jssdkSignature;
        }
    }

    public class WxJssdkSignature
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
    }
}