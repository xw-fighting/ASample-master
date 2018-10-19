using System;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
using StackExchange.Redis;

namespace DRapid.Web.VisitAnalyse.Storage.RedisMq
{
    public class RedisMqHttpVisitConsumeStore : RedisMqConsumer,
        IRedisMqConsumerHandler<HttpVisit>
    {
        public RedisMqHttpVisitConsumeStore(IOptions<RedisMqConsumerOptions> options,
            IHttpVisitStore httpVisitStore, ILogger logger) : base(options)
        {
            WriteStore = httpVisitStore;
            Logger = logger;
        }

        public IHttpVisitStore WriteStore { get; set; }

        public ILogger Logger { get; set; }

        public async void Consume(HttpVisit item, RedisChannel channel, RedisValue message)
        {
            Logger.LogInformation($"来自[{channel}]的消息抵达");
            var bulcket = TryGetBucket(item);
            if (bulcket != null)
            {
                Logger.LogInformation($"桶已满，准备持久{bulcket.Count}条数据");
                try
                {
                    var watch = Stopwatch.StartNew();
                    await WriteStore.AddAsync(bulcket);
                    watch.Stop();
                    Logger.LogInformation($"持久化完成，此次耗时{watch.ElapsedMilliseconds}毫秒");
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(new EventId(), ex, "批量持久数据失败");
                }
            }
        }

        private List<HttpVisit> TryGetBucket(HttpVisit item)
        {
            List<HttpVisit> temp = null;
            lock (this)
            {
                if (_bucket == null)
                    _bucket = new List<HttpVisit>(Options.BulcketLength);
                _bucket.Add(item);
                if (_bucket.Count == Options.BulcketLength)
                {
                    temp = _bucket;
                    _bucket = new List<HttpVisit>();
                }
            }
            return temp;
        }

        private List<HttpVisit> _bucket;

        public Task BeingConsumeAsync()
        {
            Logger.LogInformation("消费端启动，正在接收消息...");
            return RegisterHandlerAsync(new[] {this});
        }
    }
}