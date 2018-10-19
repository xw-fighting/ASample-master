using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Analysers;

namespace DRapid.Web.VisitAnalyse.Storage.Redis
{
    public class RedisHttpVisitViewAnalyser : IHttpVisitViewAnalyser
    {
        public RedisHttpVisitViewAnalyser(RedisHttpVisitStore store)
        {
            RedisHttpVisitStore = store;
        }
        public IHttpVisitStore HttpVisitStore => RedisHttpVisitStore;

        protected RedisHttpVisitStore RedisHttpVisitStore { get; set; }

        public Task<IEnumerable<HttpVisitViewAnalyseResult>> AnalyseAsync(ViewAnalyseIdentifier identifier)
        {
            var validTimePart = DateTimeParts.Day | DateTimeParts.Hour | DateTimeParts.Month;
            if ((validTimePart & identifier.DateTimePart) != identifier.DateTimePart)
            {
                throw new ArgumentException("目前仅支持按月、日、小时分析");
            }
            var timeSpan = identifier.EndTime - identifier.BeginTime;
            if (identifier.EndTime < identifier.BeginTime)
            {
                throw new ArgumentException("结束时间应该在开始时间之后");
            }
            if (identifier.EndTime.Year != identifier.BeginTime.Year)
            {
                throw new ArgumentException("不支持跨年分析");
            }
            



            throw new NotImplementedException();
        }
    }
}