namespace Thirdparty.Wechat.Service.WxApiConfig
{
    public static class WxOpenIdApis
    {
        /// <summary>
        /// 发送给特定的人，按openIds来发
        /// </summary>
        public const string SendMessageUrl =
            "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";
    }
}