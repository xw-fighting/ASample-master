using ASample.Logger.Model;
using ASample.Logger.Model.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Logger
{
    public class LoggerService
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        public static LoggerInfo OperationInfo(string title,string message)
        {
            return new LoggerInfo()
            {
                Title = title,
                Description = message,
                CreateTime = DateTime.Now,
                LoggerType = LoggerType.Operation
            };
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        public static LoggerInfo ErrorInfo(string title, string message)
        {
            return new LoggerInfo()
            {
                Title = title,
                Description = message,
                CreateTime = DateTime.Now,
                LoggerType = LoggerType.Error
            };
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        public static LoggerInfo DebuggerInfo(string title, string message)
        {
            return new LoggerInfo()
            {
                Title = title,
                Description = message,
                CreateTime = DateTime.Now,
                LoggerType = LoggerType.Debugger
            };
        }

        /// <summary>
        /// 业务日志
        /// </summary>
        public static LoggerInfo Logic(string title, string message)
        {
            return new LoggerInfo()
            {
                Title = title,
                Description = message,
                CreateTime = DateTime.Now,
                LoggerType = LoggerType.Logic
            };
        }

        //private LoggerInfo 
    }
}
