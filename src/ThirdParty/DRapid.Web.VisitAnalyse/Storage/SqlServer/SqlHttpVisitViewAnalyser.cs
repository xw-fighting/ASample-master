using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Analysers;

namespace DRapid.Web.VisitAnalyse.Storage.SqlServer
{
    public class SqlHttpVisitViewAnalyser : IHttpVisitViewAnalyser
    {
        public SqlHttpVisitViewAnalyser(IHttpVisitStore httVistStore)
        {
            HttpVisitStore = httVistStore;
        }

        public IHttpVisitStore HttpVisitStore { get; }

        public Task<IEnumerable<HttpVisitViewAnalyseResult>> AnalyseAsync(ViewAnalyseIdentifier identifier)
        {
            throw new NotImplementedException();
        }
    }
}
