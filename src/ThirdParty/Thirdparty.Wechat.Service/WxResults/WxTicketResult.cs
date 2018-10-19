using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    public class WxTicketResult : WxBaseResult
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}