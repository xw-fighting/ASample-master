using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DRapid.Utility.Exceptional.WellKnown;
using DRapid.Utility.Http;
using DRapid.Utility.Log;
using DRapid.Utility.Threading;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// 各种微信模块的基类
    /// </summary>
    public abstract class WxAbstractOperating
    {
        public WxOption WxOption { get; }

        public WxAccessTokenInfo AccessTokenInfo { get; }

        public ILogger Logger;

        protected string Token => "zAoIJ6v5ap3BEpGbwfoUb2kd-RMpG6xaYcI2ascJmqnrFKqctubf0eIZDjlOvVjNiqMbJj5qCMknYz8JKGoGXzcAraCzB3PiCYpU3W9GxpO_4-DiuAZYyj8wd5RUjZ-YCPRgAAANAC";//AccessTokenInfo.GetToken();

        protected WxAbstractOperating(WxOption option)
            : this(option, null)
        {

        }

        protected WxAbstractOperating(WxOption option, ILogger logger)
        {
            WxOption = option;

            AccessTokenInfo = new WxAccessTokenInfo(WxOption.AppId, WxOption.Secret);

            Logger = logger;
        }

        protected async Task<string> PostAsync(string url, string jsonStr)
        {
            return await PostAsync(url, new StringContent(jsonStr));
        }

        protected async Task<string> PostAsync(string url, string jsonStr, bool isJson)
        {
            return await PostAsync(url, new StringContent(jsonStr), isJson);
        }

        protected async Task<string> PostAsync(string url, HttpContent httpContent)
        {
            var logger = Logger?.CreateChildLogger("wechat.sendmessage");
            string result;
            var param = await httpContent.ReadAsStringAsync();
            try
            {
                result = await RequestSender.PostAsync(url, httpContent);
                var info = new Info("推送微信消息");
                info.AppendInfo("param", param);
                info.AppendInfo("result", result);
                logger?.Info(info.Message, info);
            }
            catch (Exception ex)
            {
                var err = new Error(ex, "推送微信消息失败");
                err.AppendInfo("param", param);
                err.AppendInfo("result", "");
                logger?.Error(err.Message, err);
                throw;
            }

            return result;
        }

        protected async Task<string> PostAsync(string url, HttpContent httpContent, bool isJson)
        {
            if (isJson)
            {
                httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            }
            return await PostAsync(url, httpContent);
        }
    }
}