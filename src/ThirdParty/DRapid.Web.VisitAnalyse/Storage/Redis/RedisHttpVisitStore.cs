using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRapid.Utility.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using DRapid.Utility.Linq.Enumerable;
using DRapid.Web.VisitAnalyse.Storage.SqlServer;

namespace DRapid.Web.VisitAnalyse.Storage.Redis
{
    /// <summary>
    /// 通过使用菲关系型存储来增加记录的存储量。
    /// 通过使用日期进行分片来达到减少数据加载的目的。
    /// 主记录使用：List[string]
    /// 分组记录使用：Hash[string]，其中Hash主体的Key是日期
    /// </summary>
    public class RedisHttpVisitStore : IHttpVisitStore
    {
        public RedisHttpVisitStore(IOptions<RedisHttpVisitStoreOptions> options)
        {
            Options = options.Value;
            _connection = ConnectionMultiplexer.Connect(Options.ConnectionString);
        }

        private IConnectionMultiplexer _connection;

        protected RedisHttpVisitStoreOptions Options { get; set; }

        protected IConnectionMultiplexer Connection
        {
            get
            {
                if (_connection.IsConnected)
                    return _connection;
                _connection = ConnectionMultiplexer.Connect(Options.ConnectionString);
                return _connection;
            }
        }

        public Task<HttpVisit> TryGetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(HttpVisit visit)
        {
            var visitToStore = (HttpVisit) visit.Clone();
            var info = visitToStore.Inforamtion;
            visitToStore.Inforamtion = null;

            var listKey = DateBasedIdentifier<HttpVisit>
                .Build(visit.Time)
                .GetStringIdentifier();

            var hashKey = TextBasedIdentifier<Dictionary<string, string>>
                .Build(visit.Id.ToString())
                .GetStringIdentifier();

            var json = JsonConvert.SerializeObject(visitToStore);
            List<HashEntry> hashEntries = null;

            if (info != null)
            {
                hashEntries = new List<HashEntry>();
                foreach (var key in info.Keys)
                {
                    var value = info[key];
                    if (value != null)
                    {
                        var entry = new HashEntry(key, info[key]);
                        hashEntries.Add(entry);
                    }
                }
            }

            var database = Connection.GetDatabase();
            var batch = database.CreateBatch();

            var itemTask = batch.ListLeftPushAsync(listKey, json);
            Task infoTask = null;
            if (hashEntries != null)
            {
                infoTask = batch.HashSetAsync(hashKey, hashEntries.ToArray());
            }
            batch.Execute();
            await itemTask;
            if (infoTask != null)
            {
                await infoTask;
            }
        }

        public Task AddAsync(IEnumerable<HttpVisit> visit)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HttpVisit>> SelectAsync(HttpVisitFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HttpVisit>> SelectByDayAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<IPaged<SqlHttpVisitInfoTable>> SelectVisitInfoPagedByIdAsync(Guid visitId, PageInfo pageInfo)
        {
            return Task.FromResult((IPaged<SqlHttpVisitInfoTable>)null);
        }

        public Task<IPaged<SqlHttpVisitTable>> SelectPagedAsync(PageInfo pageInfo, HttpVisitFilter visitFilter)
        {
            return Task.FromResult((IPaged<SqlHttpVisitTable>)null);
        }
    }
}