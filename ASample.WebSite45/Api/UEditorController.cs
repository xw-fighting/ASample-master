using ASample.ThirdParty.UEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ASample.WebSite45.Api
{
    public class UEditorController : ApiController
    {
        [HttpGet]
        public string Upload(string action,string noCache)
        {
            var context = HttpContext.Current;
            var response = UEditorService.Instance.UploadAndResponse(context);
            return response.Result.ToString()+ response.ContentType.ToString();
        }
    }
}
