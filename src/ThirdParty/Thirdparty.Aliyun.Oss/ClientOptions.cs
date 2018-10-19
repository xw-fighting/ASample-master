namespace Thirdparty.Aliyun.Oss
{
    public class ClientOptions: OssClientOptions
    {
        public ClientOptions()
        {
            AccessKeyId = "LTAIJxq3yJlcxOtN";
            AccessKeySecret = "k3rS6R9GFYJoXBV9mtgpnBL92u5jF1";
            Endpoint = "http://oss-cn-hangzhou.aliyuncs.com";
        }

        public ClientOptions(string accessKeyId,string accessKeySecret,string endpoint)
        {
            AccessKeyId = accessKeyId;
            AccessKeySecret = accessKeySecret;
            Endpoint = endpoint;
        }
    }
}