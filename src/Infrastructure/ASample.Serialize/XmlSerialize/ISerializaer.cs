
namespace ASample.Serialize.XmlSerialize
{
    public interface ISerializaer
    {
        /// <summary>
        /// 序列化：将对象转换为字符串
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        string Serialize(object instance);
    }
}
