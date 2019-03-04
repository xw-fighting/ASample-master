using ASample.ThirdParty.WeChat.WeChatPay.Core.HttpException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.WeChat.WeChatPay.Core
{
    public class HttpHelper
    {
        private static readonly HttpClient _httpClient;
        private static string BASE_ADDRESS = "localhost";
        static HttpHelper()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(BASE_ADDRESS) };
            //帮HttpClient热身
            _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod("HEAD"),
                RequestUri = new Uri(BASE_ADDRESS + "/")
            })
                .Result.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// 处理http GET请求
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>result  返回的数据</returns>
        public static string Get(string url)
        {
            System.GC.Collect();//回收没有正常关闭的http连接
            var result = "";

            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response != null)
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = sr.ReadToEnd().Trim();
                        }
                    }
                }
                request.Abort();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogs(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// 处理http POST请求
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <param name="postData">要post的数据</param>
        /// /// <param name="isUseCert">是否需要使用证书</param>
        /// <returns>result  请求到的数据</returns>
        public static string Post(string url, string postData, bool isUseCert = false)
        {
            System.GC.Collect();//回收没有正常关闭的http请求
            var result = "";

            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "text/xml";//设置post的数据类型
                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = postBytes.Length;//设置post的数据长度

                //是否使用证书
                if (isUseCert)
                {
                    var x509Cert = new X509Certificate2("", "", X509KeyStorageFlags.MachineKeySet);
                    request.ClientCertificates.Add(x509Cert);
                }

                //向服务器写数据
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response != null)
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = sr.ReadToEnd().Trim();
                        }
                    }
                }

                request.Abort();//关闭请求
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogs(ex.ToString());
            }
            //LogHelper.WriteLogs("返回结果："+ result);
            return result;
        }

        public static void PostJson(string url, string json)
        {
            System.GC.Collect();//回收没有正常关闭的http请求
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";//设置post的数据类型
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                request.ContentLength = postBytes.Length;//设置post的数据长度

                //向服务器写数据
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                }

                request.Abort();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// HttpPsot
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestContent"></param>
        /// <param name="timeOutInMs"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync (string url,HttpContent requestContent, int? timeOutInMs = null)
        {
            var timeOut = timeOutInMs != null?(TimeSpan?)TimeSpan.FromMilliseconds(timeOutInMs.Value): null;
            if (timeOut != null)
                _httpClient.Timeout = timeOut.Value;
            var response = await _httpClient.PostAsync(new Uri(url), requestContent);
            var responseText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK )
                throw new HttpResultException(response.StatusCode, responseText);
            return responseText;
        }

        /// <summary>
        /// HttpPsot
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestContent"></param>
        /// <param name="timeOutInMs"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, HttpContent requestContent, int? timeOutInMs = null)
        {
            var timeOut = timeOutInMs != null ? (TimeSpan?)TimeSpan.FromMilliseconds(timeOutInMs.Value) : null;
            if (timeOut != null)
                _httpClient.Timeout = timeOut.Value;
            var response = await _httpClient.GetAsync(new Uri(url));
            var responseText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpResultException(response.StatusCode, responseText);
            return responseText;
        }
    }
}
