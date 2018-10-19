using System.Threading.Tasks;
using Thirdparty.Wechat.Service.WxApiConfig;
using Thirdparty.Wechat.Service.WxJsonMessages;
using Thirdparty.Wechat.Service.WxModels;
using Thirdparty.Wechat.Service.WxResults;
using DRapid.Utility.Log;
using DRapid.Utility.Serialization;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service
{
    public class WxMessageManager : WxAbstractOperating
    {
        public WxMessageManager(WxOption option, ILogger logger)
            : base(option, logger)
        {

        }

        public WxMessageManager(WxOption option)
            : base(option)
        {

        }

        /// <summary>
        /// 客服发送模式，通常和订阅一起用
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<WxSendMessageResult> SendMessageByCustom(IWxCustomMessage message)
        {
            var jsonMessage = WxJsonMessageFactory.GetJsonMessage(message);
            var jsonStr = JsonConvert.SerializeObject(jsonMessage);
            var url = string.Format(WxCustomApis.SendMessageUrl, Token);
            return await PostAsync(url, jsonStr);
        }

        /// <summary>
        /// 只能发给2个以上的人
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<WxSendMessageResult> SendMessageByOpenIds(IWxOpenMessage message)
        {
            var jsonMessage = WxJsonMessageFactory.GetJsonMessage(message);
            var jsonStr = JsonConvert.SerializeObject(jsonMessage);
            var url = string.Format(WxOpenIdApis.SendMessageUrl, Token);
            return await PostAsync(url, jsonStr);
        }

        private new async Task<WxSendMessageResult> PostAsync(string url, string jsonStr)
        {
            return Json.Default.Deserialize<WxSendMessageResult>(await base.PostAsync(url, jsonStr));
        }

        /// <summary>
        /// 根据Tag发送
        /// </summary>
        /// <param name="wxTagMessage"></param>
        /// <returns></returns>
        public async Task<WxSendMessageResult> SendMessageByTag(IWxTagMessage wxTagMessage)
        {
            var jsonMessage = WxJsonMessageFactory.GetJsonMessage(wxTagMessage);
            var jsonStr = JsonConvert.SerializeObject(jsonMessage);
            var url = string.Format(WxTagApis.SendMessageUrl, Token);
            return await PostAsync(url, jsonStr);
        }

        /// <summary>
        /// 根据模板发送消息
        /// </summary>
        /// <param name="wxTemplateMessage"></param>
        /// <returns></returns>
        public async Task<WxTemplateResult> SendMessageByTemplate(WxTemplateMessage wxTemplateMessage)
        {
            var jsonStr = JsonConvert.SerializeObject(new WxJsonTemplateMessage(wxTemplateMessage));
            var url = string.Format(WxTemplateApis.SendMessageUrl, Token);
            var result = await base.PostAsync(url, jsonStr);
            return JsonConvert.DeserializeObject<WxTemplateResult>(result);
        }

        /// <summary>
        /// 发送城市服务消息通路
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<WxTemplateResult> SendMessage(string json)
        {
            var messageUrl = "https://api.weixin.qq.com/cityservice/sendmsgdata?access_token={0}";
            var url = string.Format(messageUrl, Token);
            var result = await base.PostAsync(url, json);
            return JsonConvert.DeserializeObject<WxTemplateResult>(result);
        }
    }
}