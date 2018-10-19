using System.Collections.Generic;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Storage;

namespace DRapid.Web.VisitAnalyse.Analysers
{
    public interface IHttpVisitViewAnalyser : IHttpVisitAnalyser
    {
        Task<IEnumerable<HttpVisitViewAnalyseResult>> AnalyseAsync(ViewAnalyseIdentifier identifier);
    }
}