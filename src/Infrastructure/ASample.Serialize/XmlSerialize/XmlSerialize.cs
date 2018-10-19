using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ASample.Serialize.XmlSerialize
{
    /// <summary>
    /// XML序列化类
    /// </summary>
    public class XmlSerialize : ISerializaer,IDeserializer
    {
        /// <summary>
        /// 序列化：将对象转换为字符串
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public  string Serialize(object instance)
        {
            return Serialize(instance, instance.GetType());
        }
        private  string Serialize(object obj, Type forType)
        {
            //var xml = new StringBuilder();
            //using (XmlWriter writer = XmlWriter.Create(xml))
            //{
            //    XmlSerializer xs = new XmlSerializer(obj.GetType());
            //    xs.Serialize(writer, obj);
            //    return xml.ToString();
            //}

            string xmlString = string.Empty;
            var settings = new XmlWriterSettings();
            //去除xml声明
            //settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.Encoding = Encoding.Default;
            using (MemoryStream memeryStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(memeryStream, settings))
                {
                    //去除默认命名空间xmlns:xsd和xmlns:xsi
                    var ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    var xmlSerializer = new XmlSerializer(obj.GetType());
                    xmlSerializer.Serialize(writer, obj, ns);
                }
                xmlString = Encoding.Default.GetString(memeryStream.ToArray());
            }
            return xmlString;
        }

        /// <summary>
        /// 反序列化：将xml字符串转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public T Deserialize<T> (string xmlContent) where T:class
        {
            return Deserialize(xmlContent, typeof(T)) as T;
        }

        private object Deserialize(string content, Type targetType)
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            using (var memoryStream = new MemoryStream(bytes))
            using (var reader = new XmlTextReader(memoryStream))
            {
                var xmlSerializer = new XmlSerializer(targetType);
                return xmlSerializer.Deserialize(reader);
            }
        }
    }
}
