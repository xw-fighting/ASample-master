using System;
using System.IO;
using System.Linq;
using ASample.ThirdParty.UEditor.NetCore.Models;
using ASample.ThirdParty.UEditor.NetCore.Models.Result;
using ASample.ThirdParty.UEditor.NetCore.Core;
using Microsoft.AspNetCore.Http;

namespace ASample.ThirdParty.UEditor.NetCore.Handlers
{
    public class UploadHandler : Handler
    {
        public UploadConfig UploadConfig { get; private set; }
        public UploadResult Result { get; private set; }


        public UploadHandler(HttpContext context, UploadConfig config)
            : base(context)
        {
            this.UploadConfig = config;
            this.Result = new UploadResult() { State = UploadState.Unknown };
        }

        public override UEditorResult Process()
        {
            byte[] uploadFileBytes = null;
            string uploadFileName = null;

            if (UploadConfig.Base64)
            {
                uploadFileName = UploadConfig.Base64Filename;
                uploadFileBytes = Convert.FromBase64String(Request.Form[UploadConfig.UploadFieldName]);
            }
            else
            {
                var file = Request.Form.Files[UploadConfig.UploadFieldName];
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadFileName))
                {
                    Result.State = UploadState.TypeNotAllow;
                    return WriteResult();
                }
                if (!CheckFileSize(file.Length))
                {
                    Result.State = UploadState.SizeLimitExceed;
                    return WriteResult();

                }
                uploadFileBytes = new byte[file.Length];
                try
                {
                    file.OpenReadStream().Read(uploadFileBytes, 0, (int)file.Length);
                }

                catch (Exception)
                {
                    Result.State = UploadState.NetworkError;
                    WriteResult();
                }
            }

            Result.OriginFileName = uploadFileName;

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
                Result.Url = savePath;
                Result.State = UploadState.Success;
            }
            catch (Exception e)
            {
                Result.State = UploadState.FileAccessError;
                Result.ErrorMessage = e.Message;
            }
            finally
            {
                result = WriteResult();
            }
            return result;
        }

        private UEditorResult WriteResult()
        {
            return new UEditorResult
            {
                State = GetStateMessage(Result.State),
                Url = Result.Url,
                Title = Result.OriginFileName,
                Original = Result.OriginFileName,
                Error = Result.ErrorMessage
            };
        }

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

        private bool CheckFileType(string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return UploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        private bool CheckFileSize(long size)
        {
            return size < UploadConfig.SizeLimit;
        }
    }
}
