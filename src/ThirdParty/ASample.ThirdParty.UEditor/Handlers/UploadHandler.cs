using ASample.ThirdParty.UEditor.Core;
using ASample.ThirdParty.UEditor.Models;
using ASample.ThirdParty.UEditor.Models.Configs;
using ASample.ThirdParty.UEditor.Models.OutResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ASample.ThirdParty.UEditor.Handlers
{
    public class UploadHandler : Handler
    {
        /// <summary>
        /// 上传文件配置文件
        /// </summary>
        public UploadConfig UploadConfig { get; set; }

        /// <summary>
        /// 上传返回结果
        /// </summary>
        public UploadResult UploadResult { get; set; }

        public UploadHandler(HttpContext context,UploadConfig config) : base(context)
        {
            this.UploadConfig = config;
            this.UploadResult = new UploadResult() { State = UploadState.Unknown };
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <returns></returns>
        public override UEditorResult Process()
        {
            byte[] uploadFileBytes = null;
            string uploadFileName = null;

            if (UploadConfig.IsBase64)
            {
                uploadFileName = UploadConfig.Base64Filename;
                var uploadFiledName = Request.Form[UploadConfig.UploadFieldName];
                uploadFileBytes = Convert.FromBase64String(uploadFiledName);
            }
            else
            {
                var file = Request.Files[UploadConfig.UploadFieldName];
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadFileName))
                {
                    UploadResult.State = UploadState.TypeNotAllow;
                    return ReturnResult();
                }
                if (!CheckFileSize(file.ContentLength))
                {
                    UploadResult.State = UploadState.SizeLimitExceed;
                    return ReturnResult();

                }
                uploadFileBytes = new byte[file.ContentLength];
                try
                {
                    file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
                }
                catch (Exception)
                {
                    UploadResult.State = UploadState.NetworkError;
                    ReturnResult();
                }
            }
            UploadResult.OriginFileName = uploadFileName;

            var savePath = PathFormatter.Format(uploadFileName, UploadConfig.PathFormat);
            var localPath = Path.Combine(Config.WebRootPath, savePath);
            UEditorResult result;
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }
                File.WriteAllBytes(localPath, uploadFileBytes);
                UploadResult.Url = savePath;
                UploadResult.State = UploadState.Success;
            }
            catch (Exception e)
            {
                UploadResult.State = UploadState.FileAccessError;
                UploadResult.ErrorMessage = e.Message;
            }
            finally
            {
                result = ReturnResult();
            }

            return result;
        }

        /// <summary>
        /// 校验文件大小
        /// </summary>
        /// <param name="contentLength"></param>
        /// <returns></returns>
        private bool CheckFileSize(int contentLength)
        {
            return contentLength < UploadConfig.SizeLimit;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <returns></returns>
        private UEditorResult ReturnResult()
        {
            return new UEditorResult
            {
                State = GetStateMessage(UploadResult.State),
                Url = UploadResult.Url,
                Title = UploadResult.OriginFileName,
                Original = UploadResult.OriginFileName,
                Error = UploadResult.ErrorMessage
            };
        }

        /// <summary>
        /// 获取状态消息
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private string GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }

        /// <summary>
        /// 校验文件类型
        /// </summary>
        /// <param name="uploadFileName"></param>
        /// <returns></returns>
        private bool CheckFileType(string uploadFileName)
        {
            var fileExtension = Path.GetExtension(uploadFileName).ToLower();
            return UploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }
    }
}
