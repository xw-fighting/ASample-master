using System.Web;
using System.Web.Mvc;
using DRapid.Web.VisitAnalyse.Core;

namespace DRapid.Web.VisitAnalyse.Owin
{
    public class MarkedEntryAttribute : ActionFilterAttribute
    {
        public string Key { get; set; }

        public string Description { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext.GetOwinContext();
            var httpVisit = context.Get<HttpVisit>(string.Empty);
            if (httpVisit != null)
            {
                httpVisit.Key = Key;
                httpVisit.Description = Description;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}