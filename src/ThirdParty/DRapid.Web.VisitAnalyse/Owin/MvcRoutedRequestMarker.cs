using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;

namespace DRapid.Web.VisitAnalyse.Owin
{
    public class MvcRoutedRequestMarker : ActionFilterAttribute
    {
        /// <summary>
        /// 将一个action标记为需要忽略
        /// </summary>
        public bool Ignore { get; set; }

        public const string IsMvcRoutedRequestKey = "IsMvcRoutedRequest";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.GetOwinContext().Set(IsMvcRoutedRequestKey, !Ignore);
        }

        public static Func<IOwinContext, bool> VisitInfoWhetherCollect
            = context => context.Get<bool>(IsMvcRoutedRequestKey);
    }
}