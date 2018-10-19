using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Core;
using DRapid.Utility.Linq.Enumerable;
using DRapid.Web.VisitAnalyse.Storage.SqlServer;

namespace DRapid.Web.VisitAnalyse.Storage
{
    public interface IHttpVisitStore
    {
        Task<HttpVisit> TryGetAsync(Guid id);

        Task AddAsync(HttpVisit visit);

        Task AddAsync(IEnumerable<HttpVisit> visit);

        Task<IEnumerable<HttpVisit>> SelectAsync(HttpVisitFilter filter);


        /// <summary>
        /// 通过HttpVisit 编号查询系统访问记录详细信息
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        Task<IPaged<SqlHttpVisitInfoTable>> SelectVisitInfoPagedByIdAsync(Guid visitId, PageInfo pageInfo);

        /// <summary>
        /// 分页查询系统访问记录
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        Task<IPaged<SqlHttpVisitTable>> SelectPagedAsync(PageInfo pageInfo, HttpVisitFilter visitFilter);
    }
}