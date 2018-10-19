using System;
using DRapid.Web.VisitAnalyse.Core;
using DRapid.Web.VisitAnalyse.Storage;

namespace DRapid.Web.VisitAnalyse.Analysers
{
    public class ViewAnalyseIdentifier : IIdentifier
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 进行统计的
        /// </summary>
        public DateTimeParts DateTimePart { get; set; }

        /// <summary>
        /// 指定业务标识
        /// </summary>
        public string HttpVisitKey { get; set; }

        /// <summary>
        /// 获取一个可以唯一标识该分析的字符串
        /// </summary>
        /// <returns></returns>
        public string GetStringIdentifier()
        {
            return $"[{HttpVisitKey}][{DateTimePart}]({BeginTime.GetPart(DateTimePart)},{EndTime.ToString(DateTimePart)})";
        }
    }
}
