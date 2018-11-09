using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASample.ThirdParty.UEditor.NetCore;

namespace ASample.UEditorWeb.Controllers
{
    [Route("api/UEditor")]
    public class UEditorApiController : Controller
    {
        private readonly UEditorService _ueditorService;
        public UEditorApiController(UEditorService ueditorService)
        {
            this._ueditorService = ueditorService;
        }

        [HttpGet, HttpPost]
        public ContentResult Upload()
        {
            var response = _ueditorService.UploadAndGetResponse(HttpContext);
            return Content(response.Result, response.ContentType);
        }
    }
}