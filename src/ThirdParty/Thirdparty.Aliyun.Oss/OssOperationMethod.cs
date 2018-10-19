using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Aliyun.OSS;
using Aliyun.OSS.Util;
using DRapid.Utility.Log;

namespace Thirdparty.Aliyun.Oss
{
    public class OssOperationMethod<TOptions> where TOptions : OssClientOptions, new()
    {
        private OssClient _ossClient;
        private const string BucketName = "baitu-ersystem-bucket";

        public OssOperationMethod()
        {
            var opt = new TOptions();
            _ossClient = new OssClient(opt.Endpoint, opt.AccessKeyId, opt.AccessKeySecret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="accessKeyId"></param>
        /// <param name="accessKeySecret"></param>
        public OssOperationMethod(string endpoint, string accessKeyId, string accessKeySecret)
        {
            _ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
        }

        public void UploadFileAsync(Stream fs,string key,IDictionary<string,string> meta = null)
        {
            var metadata = new ObjectMetadata();
            if (meta != null)
            {
                foreach (var keyValuePair in meta)
                {
                    metadata.UserMetadata.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            metadata.CacheControl = "No-Cache";
            metadata.ContentType = "text-html";
            

            if (!BucketExist())
            {
                _ossClient.CreateBucket(BucketName);
                //设置访问权限为公共
                _ossClient.SetBucketAcl(BucketName, CannedAccessControlList.PublicRead);
            }
            var state = "put finish";
            _ossClient.BeginPutObject(BucketName, key, fs, metadata, ar =>
            {
                _ossClient.EndPutObject(ar);
                //上传完成
            }, state);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="key">文件key，（文件名，后缀名）</param>
        /// <returns></returns>
        public Stream DownloadFile(string key)
        {
            var fileData = _ossClient.GetObject(BucketName, key);
            return fileData?.Content;
        }

        /// <summary>
        /// 验证管理BucketName是否存在
        /// </summary>
        /// <returns></returns>
        public bool BucketExist()
        {
            var exist = _ossClient.DoesBucketExist(BucketName);
            return exist;
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="key">文件key，（文件名，后缀名）</param>
        public void DeleteFile(string key)
        {
            _ossClient.DeleteObject(BucketName, key);
        }

        /// <summary>
        /// 删除多个文件
        /// </summary>
        /// <param name="keys">List<string> 文件key，（文件名，后缀名）</param>
        public void DeleteFiles(List<string> keys)
        {
            try
            {
                var request = new DeleteObjectsRequest(BucketName, keys, false);
                _ossClient.DeleteObjects(request);
            }
            catch (Exception ex)
            {

            }

        }
    }
}