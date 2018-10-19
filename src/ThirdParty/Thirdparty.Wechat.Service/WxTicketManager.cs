using System;
using System.Net;
using Thirdparty.Wechat.Service.WxApiConfig;
using Thirdparty.Wechat.Service.WxResults;
using DRapid.Utility.Http;
using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service
{
    public class WxTicketManager
    {
        private static string Ticket { get; set; }

        private static DateTime TokenExpireDateTime { get; set; }

        public WxAccessTokenInfo AccessTokenInfo { get; }

        public WxTicketManager(string appId, string secret)
        {
            AccessTokenInfo = new WxAccessTokenInfo(appId, secret);
        }

        public string GetTicket()
        {
            if (!string.IsNullOrEmpty(Ticket) && TokenExpireDateTime >= DateTime.Now)
            {
                return Ticket;
            }

            HttpStatusCode code;
            var result = RequestSender.Get(string.Format(WxBaseApis.TicketUrl, AccessTokenInfo.GetToken()), out code);
            var ticketResult = JsonConvert.DeserializeObject<WxTicketResult>(result);
            if (!ticketResult.IsSuccess)
            {
                throw new Exception("jsapiTicket 获取失败");
            }
            Ticket = ticketResult.Ticket;
            TokenExpireDateTime = DateTime.Now.AddSeconds(ticketResult.ExpiresIn);
            return Ticket;
        }
    }
}