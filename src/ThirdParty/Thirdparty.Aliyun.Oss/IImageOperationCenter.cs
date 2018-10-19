using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thirdparty.Aliyun.Oss
{
    /// <summary>
    /// 图片操作中心
    /// </summary>
    public interface IImageOperationCenter
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="key">上传文件的key</param>
        /// <returns></returns>
        Task Upload(Stream fileStream, string key);

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="key">创传文件的key</param>
        /// <param name="metaData">meta</param>
        /// <returns></returns>
        Task Upload(Stream fileStream, string key, IDictionary<string, string> metaData);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="key">文件对应的key</param>
        /// <returns></returns>
        Stream Download(string key);

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="key">文件key，（文件名，后缀名）</param>
        void Delete(string key);

        /// <summary>
        /// 删除多个文件
        /// </summary>
        /// <param name="key">文件key，（文件名，后缀名）</param>
        void Deletes(List<string> keys);
    }
}
