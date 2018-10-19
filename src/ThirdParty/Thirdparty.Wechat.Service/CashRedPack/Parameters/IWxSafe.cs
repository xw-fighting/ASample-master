namespace Thirdparty.Wechat.Service.CashRedPack.Parameters
{
    internal interface IWxSafe
    {
        string WxAppId { get; set; }

        string MchKey { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        string NonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        string Sign { get; set; }

        string ToSortedParamString();
    }
}
