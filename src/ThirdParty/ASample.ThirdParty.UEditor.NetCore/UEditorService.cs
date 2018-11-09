using Microsoft.AspNetCore.Hosting;
using ASample.ThirdParty.UEditor.NetCore.Models;
using ASample.ThirdParty.UEditor.NetCore.Models.Result;
using ASample.ThirdParty.UEditor.NetCore.Handlers;
using ASample.ThirdParty.UEditor.NetCore.Core;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Http;


namespace ASample.ThirdParty.UEditor.NetCore
{
    public class UEditorService
    {
        public UEditorService(IHostingEnvironment env)
        {
            // .net core的名字起的比较怪而已，并不是我赋值赋错了
            if (string.IsNullOrWhiteSpace(Config.WebRootPath))
            {
                Config.WebRootPath = env.ContentRootPath;
            }

            Config.EnvName = env.EnvironmentName;
        }

        /// <summary>
        /// 上传并返回结果，已处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public UEditorResponse UploadAndGetResponse(HttpContext context)
        {
            var action = context.Request.Query["action"];
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
            string jsonpCallback = context.Request.Query["callback"];
            if (!jsonpCallback.IsNullOrWhiteSpace())
            {
                contentType = "application/javascript";
                resultJson = string.Format("{0}({1});", jsonpCallback, resultJson);
                UEditorResponse response = new UEditorResponse(contentType, resultJson);
                return response;
            }
            else
            {
                UEditorResponse response = new UEditorResponse(contentType, resultJson);
                return response;
            }
        }
    }
}
