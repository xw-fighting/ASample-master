using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASample.SignalRNet.WebSite.Controllers
{
    public class SignalRController : Controller
    {
        // GET: SignalR
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult MonitoringPage()
        {
            return View();
        }
        public ActionResult EnterPage()
        {
            return View();
        }
        
    }
        
}