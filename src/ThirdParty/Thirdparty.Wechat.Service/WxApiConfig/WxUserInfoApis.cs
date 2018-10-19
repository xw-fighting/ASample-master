namespace Thirdparty.Wechat.Service.WxApiConfig
{
    public static class WxUserInfoApis
    {
        public const string GetUserInfo =
            "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";

        /// <summary>
        /// 根据标签获取用户
        /// </summary>
        public const string GetUsersByTag = "https://api.weixin.qq.com/cgi-bin/user/tag/get?access_token={0}";
    }
}