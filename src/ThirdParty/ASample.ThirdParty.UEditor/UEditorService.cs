using ASample.ThirdParty.UEditor.Core;
using ASample.ThirdParty.UEditor.Handlers;
using ASample.ThirdParty.UEditor.Models;
using ASample.ThirdParty.UEditor.Models.OutResult;
using Newtonsoft.Json;
using System;
using System.Web;

namespace ASample.ThirdParty.UEditor
{
    /// <summary>
    /// UEditor 编辑器服务入口
    /// </summary>
    public class UEditorService
    {
        private UEditorService()
        {

        }

        private static UEditorService _instance;

        public static UEditorService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UEditorService();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 上传并返回结果，已处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public UEditorResponse UploadAndResponse(HttpContext context)
        {
            var action = context.Request.QueryString["action"];
            object result;
            if (UploadType.Config.Equals(action, StringComparison.OrdinalIgnoreCase))
            {
                var configHandle = new ConfigHandler();
                result = configHandle.Process();
            }
            else
            {
                var handle = HandelFactory.GetHandler(action, context);
                result = handle.Process();
            }

            string resultJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            string contentType = "text/plain";
            string jsonpCallback = context.Request.QueryString["callback"];
            if (!jsonpCallback.IsNullOrWhiteSpace())
            {
                contentType = "application/javascript";
                resultJson = string.Format("{0}({1});", jsonpCallback, resultJson);
                var response = new UEditorResponse(contentType, resultJson);
                return response;
            }
            else
            {
                var response = new UEditorResponse(contentType, resultJson);
                return response;
            }
        }

        /// <summary>
        /// 单纯的上传并返回结果，未处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Upload(HttpContext context)
        {
            var action = context.Request.QueryString["action"];
            object result;
            if (UploadType.Config.Equals(action, StringComparison.OrdinalIgnoreCase))
            {
                result = new ConfigHandler();
            }
            else
            {
                var handle = HandelFactory.GetHandler(action, context);
                result = handle.Process();
            }
            return result;
        }
    }
}
