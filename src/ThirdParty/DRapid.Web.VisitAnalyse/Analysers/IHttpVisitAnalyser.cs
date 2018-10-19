using DRapid.Web.VisitAnalyse.Storage;

namespace DRapid.Web.VisitAnalyse.Analysers
{
    public interface IHttpVisitAnalyser
    {
        IHttpVisitStore HttpVisitStore { get; }
    }
}
