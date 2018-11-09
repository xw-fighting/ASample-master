using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Http
{
    /// <summary>
    /// httpClient 访问方法组装
    /// </summary>
    public class HttpWraper
    {
        private static readonly HttpClient _httpClient;

        private static string BASE_ADDRESS;
        static HttpWraper()
        {
            _httpClient = new HttpClient() {
                BaseAddress = new Uri("https://www.baidu.com")
            };

            //帮HttpClient热身
            _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod("HEAD"),
                RequestUri = new Uri("https://www.baidu.com")
            }) .Result.EnsureSuccessStatusCode();
        }

        public static async Task<string> PostAsync(string url,string postContent)
        {
            var content = new StringContent(postContent);
            var response = await _httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public static async Task<string> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
