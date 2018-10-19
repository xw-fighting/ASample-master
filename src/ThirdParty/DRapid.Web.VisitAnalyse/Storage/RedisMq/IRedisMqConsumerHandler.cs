using StackExchange.Redis;

namespace DRapid.Web.VisitAnalyse.Storage.RedisMq
{
    public interface IRedisMqConsumerHandler<in T>
    {
        void Consume(T item, RedisChannel channel, RedisValue message);
    }
}