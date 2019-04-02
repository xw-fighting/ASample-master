using ASample.Logger.Model.Values;
using System;

namespace ASample.Logger.Model
{
    public class LoggerInfo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }
        public LoggerType LoggerType { get; set; }
    }
}
