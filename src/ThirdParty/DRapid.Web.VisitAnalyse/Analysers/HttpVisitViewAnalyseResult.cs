using System;
using DRapid.Web.VisitAnalyse.Storage;

namespace DRapid.Web.VisitAnalyse.Analysers
{
    /// <summary>
    /// 访问分析
    /// </summary>
    public class HttpVisitViewAnalyseResult
    {
        /// <summary>
        /// 页面浏览量
        /// </summary>
        public int CountByPageView { get; set; }

        /// <summary>
        /// 用户浏览量
        /// </summary>
        public int CountByUserView { get; set; }
         
        /// <summary>
        /// 该次分析所对应的的时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 该次分析的时间部分类型
        /// </summary>
        public DateTimeParts DateTimePart { get; set; }
    }
}
  