using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Logger.Handlers
{
    public class SystemLogHandler:ILogHandler
    {
        //public static SystemLogHandler _SystemLog = new SystemLogHandler();
        public  void WriteLog(string logJson)
        {
            //写入数据库
            //mongodb
            //sqlserver
            //txt
        }

        public  void SelectLog(string filterJson)
        {

        }
    }
}
