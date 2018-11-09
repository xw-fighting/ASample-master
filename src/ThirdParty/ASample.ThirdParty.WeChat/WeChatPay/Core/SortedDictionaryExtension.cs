using System;
using System.Collections.Generic;

namespace ASample.ThirdParty.WeChat.WeChatPay.Core
{
    public static class SortedDictionaryExtension
    {
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="sortDicSource"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(this SortedDictionary<string,object> sortDicSource ,string key,string value)
        {
            sortDicSource[key]  = value;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="sortDicSource"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(this SortedDictionary<string, string> sortDicSource, string key, string value)
        {
            sortDicSource[key] = value;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="sortDicSource"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValue(this SortedDictionary<string, object> sortDicSource, string key)
        {
            object value = null;
            sortDicSource.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="sortDicSource"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValue(this SortedDictionary<string, string> sortDicSource, string key)
        {
            var value = string.Empty;
            sortDicSource.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// 将SortedDictionary 转换为字符串
        /// </summary>
        /// <param name="sortDicSource"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToUrlString(this SortedDictionary<string, object> sortDicSource)
        {
            string paramaters = "";
            foreach (KeyValuePair<string, object> kvPair in sortDicSource)
            {
                if (kvPair.Key != "sign" && kvPair.Key != "refund_channel" && kvPair.Value != null)
                {
                    paramaters += kvPair.Key + "=" + kvPair.Value + "&";
                }
            }
            paramaters = paramaters.Trim('&');
            return paramaters;
        }

        /// <summary>
        /// 将SortedDictionary 转换为XML串
        /// </summary>
        /// <param name="sortDicSource"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToXml(this SortedDictionary<string, object> sortDicSource)
        {
            if (sortDicSource.Count == 0)
            {
                throw new Exception("sortDicSource不能为空");
            }
            string xml = "<xml>";
            foreach (KeyValuePair<string, object> kvPair in sortDicSource)
            {
                if (kvPair.Value == null)
                {
                    throw new Exception("sortDicSource中的值不能包含有null");
                }

                if (kvPair.Value.GetType() == typeof(int))
                {
                    xml += "<" + kvPair.Key + ">" + kvPair.Value + "</" + kvPair.Key + ">";
                }
                else if (kvPair.Value.GetType() == typeof(string))
                {
                    xml += "<" + kvPair.Key + ">" + "<![CDATA[" + kvPair.Value + "]]></" + kvPair.Key + ">";
                }
                else
                {
                    throw new Exception("sortDicSource 除了string和int类型不能含有其他数据类型");
                }
            }
            xml += "</xml>";
            return xml;
        }
    }
}
