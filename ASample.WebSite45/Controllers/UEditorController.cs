using ASample.ThirdParty.UEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASample.WebSite45.Controllers
{
    public class UEditorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult Upload()
        {
            var context = HttpContext.ApplicationInstance.Context;
            var response = UEditorService.Instance.UploadAndResponse(context);
            return Content(response.Result, response.ContentType);
        }
    }
}