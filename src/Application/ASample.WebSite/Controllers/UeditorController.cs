using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UEditor.Core;

namespace ASample.WebSite.Controllers
{
    public class UeditorController : Controller
    {
        // GET: Ueditor
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult Upload()
        {
            var context = HttpContext.ApplicationInstance.Context;
            var response = UEditorService.Instance.UploadAndGetResponse(context);
            return Content(response.Result, response.ContentType);
        }
    }
}