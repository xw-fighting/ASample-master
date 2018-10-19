using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Core;
using DRapid.Web.VisitAnalyse.Storage;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace DRapid.Web.VisitAnalyse.Owin
{
    public class VisitInfoCollectMiddleware : OwinMiddleware
    {
        public override async Task Invoke(IOwinContext context)
        {
            var visit = VisitProvider(context);

            StartCounter(visit);
            await Next.Invoke(context);
            StopCounter(visit);

            CollectVisitInfo(context, visit);
            CollectHttpRequestInfo(context, visit);

            if (WhetherCollect(context))
                await HttpVisitStore.AddAsync(visit);
        }

        protected virtual void CollectVisitInfo(IOwinContext context, HttpVisit visit)
        {
            visit.Id = Guid.NewGuid();
            visit.Url = context.Request.Uri.ToString();
            visit.Identity = VisitIdentityProvider(context);
            visit.Method = context.Request.Method;
            visit.StatusCode = context.Response.StatusCode.ToString();
            visit.Inforamtion = new Dictionary<string, string>();
        }

        protected virtual void CollectHttpRequestInfo(IOwinContext context, HttpVisit visit)
        {
            var clientIp = context.Request.RemoteIpAddress;
            var clientPort = context.Request.RemotePort;
            var cookieJson = JsonConvert.SerializeObject(context.Request.Cookies);

            context.Request.Headers.TryGetValue("User-Agent", out string[] agentStr);
            context.Request.Headers.TryGetValue("Referer", out string[] referer);
            context.Request.Headers.TryGetValue("Host", out string[] host);

            visit.Inforamtion["clientIp"] = clientIp;
            visit.Inforamtion["clientPort"] = clientPort.ToString();
            visit.Inforamtion["cookie"] = cookieJson;
            visit.Inforamtion["userAgent"] = agentStr?.FirstOrDefault();
            visit.Inforamtion["referer"] = referer?.FirstOrDefault();
            visit.Inforamtion["host"] = host?.FirstOrDefault();

            InfoFiller?.Invoke(context, visit.Inforamtion);
        }

        protected virtual void StartCounter(HttpVisit visit)
        {
            visit.Time = DateTime.Now;
        }

        protected virtual void StopCounter(HttpVisit visit)
        {
            var span = DateTime.Now - visit.Time;
            visit.Expires = (long) span.TotalMilliseconds;
        }

        public Func<IOwinContext, HttpVisit> VisitProvider { get; }

        public Action<IOwinContext, Dictionary<string, string>> InfoFiller { get; }

        public Func<IOwinContext, bool> WhetherCollect { get; }

        public Func<IOwinContext, VisitIdentity> VisitIdentityProvider { get; }

        public IHttpVisitStore HttpVisitStore { get; }

        public VisitInfoCollectMiddleware(OwinMiddleware next, VisitInfoCollectOptions options) : base(next)
        {
            VisitProvider = options.VisitProvider ?? DefaultVisitProvider;
            InfoFiller = options.InfoFiller ?? DefaultInfoFiller;
            HttpVisitStore = options.StoreBuilder();
            WhetherCollect = options.WhetherCollect ?? DefaultWhetherCollect;
            VisitIdentityProvider = options.VisitIdentityProvider ?? DefaultVisitIdentityProvider;
        }

        public static Func<IOwinContext, HttpVisit> DefaultVisitProvider = context =>
        {
            var result = context.Get<HttpVisit>(string.Empty);
            if (result == null)
            {
                result = new HttpVisit();
                context.Set(string.Empty, result);
            }
            return result;
        };

        public static Action<IOwinContext, Dictionary<string, string>> DefaultInfoFiller
            = (context, visit) => { };

        public static Func<IOwinContext, bool> DefaultWhetherCollect = context => true;

        public static Func<IOwinContext, VisitIdentity> DefaultVisitIdentityProvider = context =>
            VisitIdentity.Build(context.Authentication?.User?.Identity);
    }
}