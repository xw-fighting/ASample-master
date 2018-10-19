using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Analysers;

namespace DRapid.Web.VisitAnalyse.Storage.Redis
{
    public class RedisHttpVisitAnalyseStore : IHttpVisitAnalyseStore<HttpVisitViewAnalyseResult>
    {
        public Task<HttpVisitViewAnalyseResult> TryGetAsync(IIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IIdentifier identifier, HttpVisitViewAnalyseResult item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HttpVisitViewAnalyseResult>> LoadAsync(IEnumerable<IIdentifier> identifieds)
        {
            throw new NotImplementedException();
        }
    }
}
