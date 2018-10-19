using ASample.WebSite.Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ASample.WebSite.Core.Extensions
{
    public static class RequestCultureInfoMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestCultureInfo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureInfoMiddleware>();
        }
    }
}
