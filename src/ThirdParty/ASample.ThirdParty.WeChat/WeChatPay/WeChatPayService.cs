using ASample.Serialize.XmlSerialize;
using ASample.ThirdParty.WeChat.WeChatPay.Core;
using ASample.ThirdParty.WeChat.WeChatPay.Models.InputParam;
using ASample.ThirdParty.WeChat.WeChatPay.Models.OutResult;
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
        public async Task<UnifiedOrderResult> UnifiedOrder(string trade_type)
        {
            var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            var dataDic = new SortedDictionary<string, object>();
            dataDic.SetValue("AppId", "");//配置文件读取
            dataDic.SetValue("MchId", "");//配置文件读取
            dataDic.SetValue("NonceStr", WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("Body", "");//传入或者配置
            dataDic.SetValue("OutTradeNo", WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("TotalFee", "");//传入
            dataDic.SetValue("SpbillCreateIp", "");//
            dataDic.SetValue("NotifyUrl", "");//配置读取
            if(trade_type == "JSAPI")
            {
                dataDic.SetValue("OpenId", "");
            }
            //可选参数
            dataDic.SetValue("goods_tag", "xw_test");
            dataDic.SetValue("attach", "xw_test");
            dataDic.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            dataDic.SetValue("time_expire", DateTime.Now.AddMinutes(40).ToString("yyyyMMddHHmmss"));

            var sign =  WeChatPayUtility.MakeSign(dataDic.ToUrlString());
            dataDic.SetValue("sign", sign);

            var httpClient = new HttpClient();
            var content = new StringContent(dataDic.ToXml());
            var response = await httpClient.PostAsync(url, content);
            var xmlResult =await response.Content.ReadAsStringAsync();

            var xmlSerialize = new XmlSerialize();
            var result = xmlSerialize.Deserialize<UnifiedOrderResult>(xmlResult);
            return result;

        }

        /// <summary>
        /// 扫码支付
        /// </summary>
        /// <returns></returns>
        public async Task<string> CodePay()
        {
            var result = await UnifiedOrder("NATIVE");
            if (result.ReturnCode == "SUCCESS" && result.ResultCode == "SUCCESS")
            {
                return result.CodeUrl;
            }
            return string.Empty;
        }

        public async Task<QueryOrderResult> QueryOrder()
        {
            var url = "https://api.mch.weixin.qq.com/pay/orderquery";
            var dataDic = new SortedDictionary<string, object>();
            dataDic.SetValue("appid", "");//配置
            dataDic.SetValue("mch_id", "");//配置
            dataDic.SetValue("out_trade_no", "");
            dataDic.SetValue("transaction_id", "");
            dataDic.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("sign", WeChatPayUtility.MakeSign(dataDic.ToUrlString()));

            var xml = dataDic.ToXml();
            var content = new StringContent(xml);
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, content);
            var xmlResult =await  response.Content.ReadAsStringAsync();
            var xmlSer = new XmlSerialize();
            var result = xmlSer.Deserialize<QueryOrderResult>(xmlResult);
            return result;
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <returns></returns>
        public async Task<CloseOrderResult> CloseOrder(string out_trade_no)
        {
            var url = "https://api.mch.weixin.qq.com/pay/closeorder";
            var dataDic = new SortedDictionary<string, object>();
            dataDic.SetValue("appid", "");//配置
            dataDic.SetValue("mch_id", "");//配置
            dataDic.SetValue("out_trade_no", out_trade_no);//传值
            dataDic.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());
            dataDic.SetValue("sign", WeChatPayUtility.MakeSign(dataDic.ToUrlString()));

            var xml = dataDic.ToXml();
            var content = new StringContent(xml);
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, content);
            var xmlResult = await response.Content.ReadAsStringAsync();
            var xmlSer = new XmlSerialize();
            var result = xmlSer.Deserialize<CloseOrderResult>(xmlResult);
            return result;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="total_fee"></param>
        /// <param name="refund_fee"></param>
        public async Task<RefundResult> Refund(string out_trade_no, string total_fee, string refund_fee)
        {
            try
            {
                var url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
                var data = new SortedDictionary<string,object>();
                data.SetValue("appid", "");
                data.SetValue("mch_id", "");
                data.SetValue("nonce_str", WeChatPayUtility.GeneratorNonceStr());
                data.SetValue("out_trade_no", out_trade_no);
                data.SetValue("out_refund_no", WeChatPayUtility.GenerateOutTradeNo());
                data.SetValue("total_fee", total_fee);
                data.SetValue("refund_fee", refund_fee);
                data.SetValue("sign", WeChatPayUtility.MakeSign(data.ToUrlString()));

                var xml = data.ToXml();
                var httpClient = new HttpClient();
                var content = new StringContent(xml);
                var response = await httpClient.PostAsync(url, content);
                var xmlResult =await response.Content.ReadAsStringAsync();
                var xmlSerialize = new XmlSerialize();
                var result = xmlSerialize.Deserialize<RefundResult>(xmlResult);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
