namespace ASample.Thirdpary.Tencent.Identity.TxInputPara
{
    /// <summary>
    /// 腾讯签名参数
    /// </summary>
    public class TxSignatureInfo
    {
        /// <summary>
        /// appid
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string Sercert { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        public string SignKey { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        public int Expired { get; set; }
    }
}
