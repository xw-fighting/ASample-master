using ASample.Serialize.XmlSerialize;
using ASample.ThirdParty.WeChat.WeChatPay.Models.InputParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.WeChat.WeChatPay
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WeChatPayService
    { 
        
        /// <summary>
        /// 统一下单
        /// </summary>
        public async Task UnifiedOrder(string appId)
        {
            var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            var xmlSerialize = new XmlSerialize();
            var param = new UnifiedOrderParam
            {
                AppId = appId,
                //MchId = wec,
                NonceStr = "",
                Sign = "",
                Body = "",
                OutTradeNo = "",
                TotalFee = "",
                SpbillCreateIp ="",
                NotifyUrl = "",
                TradeType = "",
                OpenId = ""
            };
            var xml = xmlSerialize.Serialize(param);
            var httpClient = new HttpClient();
            var content = new StringContent(xml);
            var response = await httpClient.PostAsync(url, content);
            var result = response.Content;
        }
    }
}
