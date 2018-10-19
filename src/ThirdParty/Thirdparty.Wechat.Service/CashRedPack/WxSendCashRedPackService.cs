using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Thirdparty.Wechat.Service.CashRedPack.Parameters;
using Thirdparty.Wechat.Service.CashRedPack.Parameters.Reuqest;
using Thirdparty.Wechat.Service.CashRedPack.Results;
using Thirdparty.Wechat.Service.WxApiConfig;
using DRapid.Utility.Hash;

namespace Thirdparty.Wechat.Service.CashRedPack
{
    /// <summary>
    /// 微信现金红包服务类
    /// </summary>
    public class WxSendCashRedPackService
    {
        /// <summary>
        /// 发送普通红包
        /// </summary>
        /// <param name="busiParam"></param>
        /// <param name="cert"></param>
        /// <returns></returns>
        public async Task<GeneralRedPackResult> SendGeneralRedPackAsync(GeneralRedPackParameter busiParam
            , X509Certificate2 cert)
        {
            var reqParam = new GeneralRedPackRequestParam(busiParam);
            var result = await InvokeAsync<GeneralRedPackRequestParam, GeneralRedPackResult>(reqParam, cert, WxCashRedPackApis.GeneralRedpackUrl);
            return result;
        }
  
        /// <summary>
        /// 发送裂变(组)红包
        /// </summary>
        /// <param name="cert"></param>
        /// <param name="busiParam"></param>
        /// <returns></returns>
        public async Task<GroupRedPackResult> SendGroupRedPackAsync(GroupRedPackParameter busiParam
            , X509Certificate2 cert)
        {
            var reqParam = new GroupRedPackRequestParam(busiParam);
            var result = await InvokeAsync<GroupRedPackRequestParam, GroupRedPackResult>(reqParam, cert, WxCashRedPackApis.GeneralRedpackUrl);
            return result;
        }

        /// <summary>
        /// 查询红包记录
        /// </summary>
        /// <param name="busiParam"></param>
        /// <param name="cert"></param>
        /// <returns></returns>
        public async Task<RedRecordQueryResult> SelectRedPackRecordAsync(RedRecordQueryParameter busiParam
            , X509Certificate2 cert)
        {
            var reqParam = new RedRecordQueryRequestParam(busiParam);
            var result = await InvokeAsync<RedRecordQueryRequestParam, RedRecordQueryResult>(reqParam, cert, WxCashRedPackApis.QueryRedpackRecordUrl);
            return result;
        }
 
        private async Task<TResult> InvokeAsync<TParam, TResult>(TParam reqParam, X509Certificate2 cert, string url) where TParam : IWxSafe, new() where TResult : new()
        {
            reqParam.NonceStr = DateTime.Now.Ticks.ToString("x2");
            var paramString = reqParam.ToSortedParamString();
            var stringSignTemp = paramString + "&key=" + reqParam.MchKey;
            var sign = new MD5Hasher(MD5Hasher.MD5HashMode.Upper, Encoding.UTF8).Hash(stringSignTemp);
            reqParam.Sign = sign;
            var raw = DRapid.Utility.Serialization.Xml.Clean.Serialize(reqParam);

            var handler = new WebRequestHandler();
            handler.ClientCertificates.Add(cert);
            var httpClient = new HttpClient(handler);
            var response = await httpClient.PostAsync(url, new StringContent(raw, Encoding.UTF8));
            var respContent = await response.Content.ReadAsStringAsync();
            var result = DRapid.Utility.Serialization.Xml.Clean.Deserialize<TResult>(respContent);
            return result;
        }

    }
}
