namespace Thirdparty.Wechat.Service.WxApiConfig
{
    public static class WxTagApis
    {
        public const string TagCreate = "https://api.weixin.qq.com/cgi-bin/tags/create?access_token={0}";

        public const string TagDelete = "https://api.weixin.qq.com/cgi-bin/tags/delete?access_token={0}";

        public const string TagUpdate = "https://api.weixin.qq.com/cgi-bin/tags/update?access_token={0}";

        public const string TagGet = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token={0}";

        /// <summary>
        /// 批量打标签
        /// </summary>
        public const string BatchTagging =
            "https://api.weixin.qq.com/cgi-bin/tags/members/batchtagging?access_token={0}";

        /// <summary>
        /// 批量取消标签
        /// </summary>
        public const string BatchUntagging =
            "https://api.weixin.qq.com/cgi-bin/tags/members/batchuntagging?access_token={0}";
 
        /// <summary>
        /// 获取用户上面的标签
        /// </summary>
        public const string GetidList = "https://api.weixin.qq.com/cgi-bin/tags/getidlist?access_token={0}";

        /// <summary>
        /// 发送消息的接口地址
        /// </summary>
        public const string SendMessageUrl = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
    }
}