using Newtonsoft.Json;

namespace Thirdparty.Wechat.Service.WxResults
{
    public class WxBaseResult
    {
        [JsonProperty("errcode")]
        public string Errcode { get; set; }

        [JsonProperty("errmsg")]
        public string Errmsg { get; set; }

        public bool IsSuccess => Errcode == "0";

        public static WxBaseResult Succes()
        {
            return new WxBaseResult
            {
                Errcode = "0",
                Errmsg = string.Empty
            };
        }
    }
}