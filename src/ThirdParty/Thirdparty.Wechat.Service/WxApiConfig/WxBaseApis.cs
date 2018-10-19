namespace Thirdparty.Wechat.Service.WxApiConfig
{
    public static class WxBaseApis
    {
        /// <summary>
        /// 获取access_token地址
        /// </summary>
        public const string AccesstokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";


        public const string TicketUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
    }
}