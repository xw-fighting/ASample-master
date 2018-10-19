using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DRapid.Utility.Threading.Tasks;

namespace Thirdparty.Aliyun.Oss
{
    public class ImageOperationCenter<TOptions> : IImageOperationCenter where TOptions : OssClientOptions, new()
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        public Task Upload(Stream fileStream, string key)
        {
            var client = new OssOperationMethod<TOptions>();
            client.UploadFileAsync(fileStream, key);
            return DoneTask.Done;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="key">文件key</param>
        /// <param name="metaData">meta</param>
        /// <returns></returns>
        public Task Upload(Stream fileStream, string key, IDictionary<string, string> metaData)
        {
            if (metaData == null)
            {
                return Upload(fileStream, key);
            }
            var client = new OssOperationMethod<TOptions>();
            client.UploadFileAsync(fileStream, key, metaData);
            return DoneTask.Done;
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="key">文件的key</param>
        /// <returns></returns>
        public Stream Download(string key)
        {
            var client = new OssOperationMethod<TOptions>();
            var stream = client.DownloadFile(key);
            return stream;
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="key">文件key，（文件名，后缀名）</param>
        public void Delete(string key)
        {
            var client = new OssOperationMethod<ClientOptions>();
            client.DeleteFile(key);
        }

        /// <summary>
        /// 删除多个文件
        /// </summary>
        /// <param name="key">文件key，（文件名，后缀名）</param>
        public void Deletes(List<string> keys)
        {
            var client = new OssOperationMethod<ClientOptions>();
            client.DeleteFiles(keys);
        }
    }

    public class ImageOperationCenter : ImageOperationCenter<ClientOptions>
    {
        
    }
}