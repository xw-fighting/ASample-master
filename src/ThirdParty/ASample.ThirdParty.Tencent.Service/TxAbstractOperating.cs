using System;
using DRapid.Utility.Http;
using DRapid.Utility.Log;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DRapid.Utility.Exceptional.WellKnown;
using DRapid.Utility.Http.Exceptions;

namespace ASample.Thirdpary.Tencent.Identity
{
    /// <summary>
    /// 腾讯接口操作类
    /// </summary>
    public  class TxAbstractOperating
    {

        public  string SiUserId = "siUserId";

        public TxAbstractOperating(ILogger logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; set; }


        protected async Task<string> PostAsync(string url, string jsonStr,Dictionary<string,string> dicHeadInfo, bool isJson)
        {
            return await PostAsync(url, new StringContent(jsonStr), dicHeadInfo, isJson);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="httpContent">内容</param>
        /// <param name="dicHeadInfo">头部信息</param>
        /// <param name="isJson">是否是Json</param>
        /// <returns></returns>
        protected async Task<string> PostAsync(string url, HttpContent httpContent, Dictionary<string, string> dicHeadInfo, bool isJson)
        {
            ILogger logger;
            if (Logger == null)
            {
                logger = null;
            }
            else
            {
               logger =  Logger.CreateChildLogger("tencent.facedetection");
            }
            string result = string.Empty;
            try
            {
                if (isJson)
                {
                    httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                }
                if (dicHeadInfo.Count > 0)
                {
                    foreach (KeyValuePair<string, string> temp in dicHeadInfo)
                    {
                        httpContent.Headers.Add(temp.Key, temp.Value);
                    }
                }
                result = await RequestSender.PostAsync(url, httpContent, true);
                var info = new Info("收到人脸识别响应内容");
                info.AppendInfo("url", url);
                info.AppendInfo("siuserid", SiUserId);
                if (result.Length > 2000)
                {
                    info.AppendInfo("response", result.Substring(0, 2000));
                }
                else
                {
                    info.AppendInfo("response", result);
                }
                logger.IfNotNull(i => i.Info(info.Message, info));
                return result;
            }
            catch (NotOkayStatusException ex)
            {
                var err = new Error(ex, "发送人脸识别请求失败");
                err.AppendInfo("url", url);
                err.AppendInfo("siuserid", SiUserId);
                err.AppendInfo("response", result);
                err.AppendInfo("statuscode", ex.StatusCode.ToString());
                logger.IfNotNull(i => i.Error(err.Message, err));
                throw;
            }
        }

        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="encryptText">内容</param>
        /// <param name="encryptKey">秘钥</param>
        /// <returns></returns>
        public static byte[] Hmacsha1Encrypt(string encryptText, string encryptKey)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.ASCII.GetBytes(encryptKey);
            byte[] dataBuffer = Encoding.ASCII.GetBytes(encryptText);
            return hmacsha1.ComputeHash(dataBuffer);
        }

        /// <summary>
        /// md5 加密
        /// </summary>
        /// <param name="str">要加密字符串</param>
        /// <param name="code">位数</param>
        /// <returns></returns>
        public string Md5Encrypt(string str, int code)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] b = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder strB = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
                strB.Append(b[i].ToString("x").PadLeft(2, '0'));

            if (code == 16)
                return strB.ToString(8, 16).ToLower();
            else
                return strB.ToString().ToLower();

        }



    }
}
