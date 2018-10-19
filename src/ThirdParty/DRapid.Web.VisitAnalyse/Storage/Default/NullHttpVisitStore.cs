using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRapid.Utility.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Core;
using DRapid.Utility.Linq.Enumerable;
using DRapid.Web.VisitAnalyse.Storage.SqlServer;

namespace DRapid.Web.VisitAnalyse.Storage.Default
{
    public class NullHttpVisitStore : IHttpVisitStore
    {
        public Task<HttpVisit> TryGetAsync(Guid id)
        {
            return Task.FromResult((HttpVisit)null);
        }

        public Task AddAsync(HttpVisit visit)
        {
            return DoneTask.Done;
        }

        public Task AddAsync(IEnumerable<HttpVisit> visit)
        {
            return DoneTask.Done;
        }

        public Task<IEnumerable<HttpVisit>> SelectAsync(HttpVisitFilter filter)
        {
            return Task.FromResult((IEnumerable<HttpVisit>) null);
        }

        /// <summary>
        /// 通过HttpVisitId查询系统访问记录
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>

        public Task<IPaged<SqlHttpVisitInfoTable>> SelectVisitInfoPagedByIdAsync(Guid visitId, PageInfo pageInfo)
        {
            return Task.FromResult((IPaged<SqlHttpVisitInfoTable>)null);
        }

        /// <summary>
        ///  分页查询系统访问记录
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="visitFilter"></param>
        /// <returns></returns>
        public Task<IPaged<SqlHttpVisitTable>> SelectPagedAsync(PageInfo pageInfo, HttpVisitFilter visitFilter = null)
        {
            return Task.FromResult((IPaged<SqlHttpVisitTable>)null);
        }
    }
}
