namespace Thirdparty.Wechat.Service.WxApiConfig
{
    public static class WxCustomApis
    {
        /// <summary>
        /// 发送给特定的人，按openIds来发
        /// </summary>
        public const string SendMessageUrl =
            "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
    }
}