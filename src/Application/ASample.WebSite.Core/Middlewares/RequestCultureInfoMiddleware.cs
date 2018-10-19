using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;

namespace ASample.WebSite.Core.Middlewares
{
    public class RequestCultureInfoMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestCultureInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
