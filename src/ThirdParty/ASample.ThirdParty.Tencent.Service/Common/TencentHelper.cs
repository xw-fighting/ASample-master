using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using ASample.Thirdpary.Tencent.Identity.TxReslts;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace ASample.Thirdpary.Tencent.Identity.Common
{
    public class TencentHelper
    {
        /// <summary>
        /// 跟过文件流生成base64
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string EncodingBase64(Stream stream)
        {
            string baseStr;
            using (BinaryReader binreader = new BinaryReader(stream))
            {
                byte[] bytes = binreader.ReadBytes(Convert.ToInt32(stream.Length));
                baseStr = Convert.ToBase64String(bytes);
            }
            return baseStr;
        }

        /// <summary>
        /// rsa公钥解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="secretPath"></param>
        /// <returns></returns>
        public static  Task<TxLiveCheckUserInfoResult> EncryptedData(string data,string secretPath)
        {
            var dataBytes =  Convert.FromBase64String(data);
            RsaKeyParameters parameter;

            using (var reader = File.OpenText(secretPath))
            {
                var readerPem = new Org.BouncyCastle.OpenSsl.PemReader(reader);
                var readerObject = readerPem.ReadObject();
                parameter = (RsaKeyParameters) readerObject;
            }
            var decryptEngine = new Pkcs1Encoding(new RsaEngine());
            decryptEngine.Init(false, parameter);
            var blockSize = decryptEngine.GetInputBlockSize();
            string result;

            var block = 0;
            using (var ms = new MemoryStream())
            {
                while (block * blockSize < dataBytes.Length)
                {
                    var bytes = decryptEngine.ProcessBlock(dataBytes, block * blockSize, blockSize);
                    ms.Write(bytes, 0, bytes.Length);
                    block++;
                }

                ms.Seek(0, SeekOrigin.Begin);
                var array = ms.ToArray();
                result = Encoding.UTF8.GetString(array);
            }
            var resultInfo = JsonConvert.DeserializeObject<TxLiveCheckUserInfoResult>(result);
            return Task.FromResult(resultInfo);
        }

        /// <summary>
        /// 对任意类型的文件进行base64加码
        /// </summary>
        /// <param name="filePath">文件的路径和文件名 </param>
        /// <returns>对文件进行base64编码后的字符串 </returns>
        public static string FileToString(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                FileStream fs = File.OpenRead(filePath);
                BinaryReader br = new BinaryReader(fs);

                string base64String = Convert.ToBase64String(br.ReadBytes((int)fs.Length));

                br.Close();
                fs.Close();
                return base64String;
            }
            else
            {
                return "";
            }
        }
    }
}

