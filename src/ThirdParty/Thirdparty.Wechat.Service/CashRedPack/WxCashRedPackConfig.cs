using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DRapid.Utility.Configuration;
using DRapid.Utility.Configuration.Lightweight;
using DRapid.Utility.Exceptional.WellKnown;
using DRapid.Utility.Serialization;

namespace Thirdparty.Wechat.Service.CashRedPack
{
    /// <summary>
    /// 微信红包配置
    /// </summary>
    public class WxCashRedPackConfig : IConfigurationContent
    {
        /// <summary>
        /// 商户号(微信支付分配的商户号)
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户秘钥
        /// </summary>
        public string MchKey { get; set; }

        /// <summary>
        /// 商户名称（红包发送者名称）
        /// </summary>
        public string SendName { get; set; }

        /// <summary>
        /// 商家公众账号appid
        /// </summary>
        public string WxAppId { get; set; }

        public string WxPayCertSecret { get; set; }

        public string WxPayCertPath { get; set; }

        public string ClientIp { get; set; }

        /// <summary>
        /// 资金授权商户号
        /// </summary>
        public string ConsumeMchId { get; set; }

        public X509Certificate2 CreateCert
        {
            get
            {
                string path = string.Empty;
                string password = WxPayCertSecret;
                try
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, WxPayCertPath);
                    return new X509Certificate2(path, WxPayCertSecret,
                        X509KeyStorageFlags.Exportable);
                }
                catch (Exception e)
                {
                    var error = new Error(e, "加载证书发生了错误");
                    error.AppendInfo("Cert_Path",path);
                    error.AppendInfo("Password", password);
                    error.AppendInfo("InnerException", e.InnerException?.Message);
                    error.AppendInfo("InnerException2", e.InnerException?.InnerException?.Message);
                    error.AppendInfo("exception", e.Message);
                    throw error;
                }
            }
        }

        public static WxCashRedPackConfig Default => ConfigurationReader.Read<WxCashRedPackConfig>(Xml.Clean);
    }
}
