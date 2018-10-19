using System.Collections.Generic;
using System.Threading.Tasks;

namespace DRapid.Web.VisitAnalyse.Storage
{
    /// <summary>
    /// 分析结果的存储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHttpVisitAnalyseStore<T>
    {
        Task<T> TryGetAsync(IIdentifier identifier);

        Task UpdateAsync(IIdentifier identifier, T item);

        Task<IEnumerable<T>> LoadAsync(IEnumerable<IIdentifier> identifieds);
    }
}
