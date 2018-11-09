using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ASample.ThirdParty.WeChat.WeChatPay.Core
{
    public class WeChatPayUtility
    {
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GeneratorNonceStr()
        {
            var nonce_str = Guid.NewGuid().ToString().Replace("-", "");
            return nonce_str;
        }

        /// <summary>
        /// 生成时间戳，自1970年1月1日 0点0分0秒以来的秒数
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 根据当前系统时间加随机序列来生成订单号
        /// </summary>
        /// <returns></returns>
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), ran.Next(999999));
        }

        /// <summary>
        /// 使用SHA1加密字符串
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string SHA1_Encrypt(string content)
        {
            byte[] StrRes = Encoding.Default.GetBytes(content);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <returns></returns>
        public static string MakeSign(string signStr)
        {
            //使用URL键值对的格式（即key1=value1&key2=value2…）拼接成字符串str
            signStr += "&key=" + "";//配置文件中获取//在str后加入API Key
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(signStr));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("X2"));//转成十六进制，x2输出小写字母，X2输出大写字母
            }
            return sb.ToString();
        }

        /// <summary>
        /// 把微信服务端返回的数据转成KeyValue值对
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static SortedDictionary<string, object> ToKeyValues(string xml)
        {
            var sortDic = new SortedDictionary<string, object>();
            if (string.IsNullOrEmpty(xml)) throw new Exception("返回的xml数据为空");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                sortDic[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }
            return sortDic;

        }

        /// <summary>
        /// 构造url的后面的参数
        /// </summary>
        /// <param name="sortDic"></param>
        /// <returns></returns>
        public static string ToUrlParam(SortedDictionary<string, object> sortDic)
        {
            string paramaters = "";
            foreach (KeyValuePair<string, object> kvPair in sortDic)
            {
                if (kvPair.Key != "sign" && kvPair.Key != "refund_channel" && kvPair.Value != null)
                {
                    paramaters += kvPair.Key + "=" + kvPair.Value + "&";
                }
            }
            paramaters = paramaters.Trim('&');
            return paramaters;
        }
    }
}
