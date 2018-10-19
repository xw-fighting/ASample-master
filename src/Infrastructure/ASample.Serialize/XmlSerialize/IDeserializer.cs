
namespace ASample.Serialize.XmlSerialize
{
    public interface IDeserializer
    {
        /// <summary>
        /// 反序列化：将xml字符串转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        T Deserialize<T>(string xmlContent) where T : class;
    }
}
