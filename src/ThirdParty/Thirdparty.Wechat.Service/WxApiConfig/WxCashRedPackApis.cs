namespace Thirdparty.Wechat.Service.WxApiConfig
{
    public static class WxCashRedPackApis
    {
        /// <summary>
        /// 普通红包发送接口地址
        /// </summary>
        public const string GeneralRedpackUrl =
            "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";

        /// <summary>
        /// 裂变（群组）红包发送接口地址
        /// </summary>
        public const string GroupRedpackUrl =
            "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";

        /// <summary>
        /// 查询红包记录
        /// </summary>
        public const string QueryRedpackRecordUrl =
            "https://api.mch.weixin.qq.com/mmpaymkttransfers/gethbinfo";
        
    }
}