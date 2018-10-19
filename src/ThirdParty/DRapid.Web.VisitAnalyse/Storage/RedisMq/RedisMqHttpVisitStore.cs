using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Core;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using DRapid.Utility.Linq.Enumerable;
using DRapid.Web.VisitAnalyse.Storage.SqlServer;

namespace DRapid.Web.VisitAnalyse.Storage.RedisMq
{
    public class RedisMqHttpVisitStore : IHttpVisitStore
    {
        public RedisMqHttpVisitStore(IOptions<RedisMqHttpVisitStoreOptions> options)
        {
            Options = options.Value;
            var connection = ConnectionMultiplexer.Connect(Options.ConnectionString);
            _subscriber = connection.GetSubscriber();
        }

        public RedisMqHttpVisitStoreOptions Options { get; set; }

        public IHttpVisitStore QueryStore { get; set; }


        private ISubscriber _subscriber;

        public Task<HttpVisit> TryGetAsync(Guid id)
        {
            throw new NotSupportedException();
        }

        public Task AddAsync(HttpVisit visit)
        {
            return _subscriber.PublishAsync(visit, CommandFlags.FireAndForget);
        }

        public Task AddAsync(IEnumerable<HttpVisit> visit)
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<HttpVisit>> SelectAsync(HttpVisitFilter filter)
        {
            throw new NotSupportedException();
        }

        public Task<IPaged<SqlHttpVisitInfoTable>> SelectVisitInfoPagedByIdAsync(Guid visitId, PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public Task<IPaged<SqlHttpVisitTable>> SelectPagedAsync(PageInfo pageInfo, HttpVisitFilter visitFilter = null)
        {
            throw new NotImplementedException();
        }
    }
}