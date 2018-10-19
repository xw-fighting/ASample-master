using System;
using System.Collections.Generic;
using DRapid.Web.VisitAnalyse.Core;
using DRapid.Web.VisitAnalyse.Storage;
using Microsoft.Owin;

namespace DRapid.Web.VisitAnalyse.Owin
{
    public class VisitInfoCollectOptions
    {
        public VisitInfoCollectOptions(Func<IHttpVisitStore> builder)
        {
            StoreBuilder = builder;
        }

        public Func<IOwinContext, HttpVisit> VisitProvider { get; set; }

        public Func<IOwinContext, bool> WhetherCollect { get; set; }

        public Action<IOwinContext, Dictionary<string, string>> InfoFiller { get; set; }

        public Func<IHttpVisitStore> StoreBuilder { get; set; }

        public Func<IOwinContext, VisitIdentity> VisitIdentityProvider { get; set; }
    }
}
