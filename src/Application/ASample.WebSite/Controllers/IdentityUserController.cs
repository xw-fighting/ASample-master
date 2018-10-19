using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASample.WebSite.Controllers
{
    public class IdentityUserController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {
            return View(); 
        }
    }
}