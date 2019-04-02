using ASample.Logger.Handlers;
using ASample.Logger.Models.Value;

namespace ASample.Logger
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public class LogService
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="logJson"></param>
        public static void LogWriteHandler(LogType logType,string logJson)
        {
            switch (logType)
            {
                case LogType.SystemLog:
                    var systemHandler = new SystemLogHandler();
                    systemHandler.WriteLog(logJson);
                    break;

                case LogType.UserLog:
                    var userHandler = new UserLogHandler();
                    userHandler.WriteLog(logJson);
                    break;

                case LogType.ApiLog:
                    var apiHandler = new ApiLogHandler();
                    apiHandler.WriteLog(logJson);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="filterJson"></param>
        public static void LogReadHandler(LogType logType,string filterJson)
        {
            switch (logType)
            {
                case LogType.SystemLog:
                    var systemHandler = new SystemLogHandler();
                    systemHandler.SelectLog(filterJson);
                    break;
                case LogType.UserLog:
                    var userHandler = new UserLogHandler();
                    userHandler.SelectLog(filterJson);
                    break;
                case LogType.ApiLog:
                    var apiHandler = new ApiLogHandler();
                    apiHandler.SelectLog(filterJson);
                    break;
                default:
                    break;
            }
        }
    }
}
