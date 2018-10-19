using System;
using System.ComponentModel;

namespace DRapid.Web.VisitAnalyse.Storage
{
    [Flags]
    public enum DateTimeParts
    {
        [Description("年")] Year = 1,
        [Description("月")] Month = 1 << 1,
        [Description("日")] Day = 1 << 2,
        [Description("小时")] Hour = 1 << 3,
        [Description("分钟")] Minute = 1 << 4,
        [Description("秒")] Second = 1 << 5,
        [Description("毫秒")] Millisecond = 1 << 6
    }
}
