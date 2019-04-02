using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Logger.Handlers
{
    public interface ILogHandler
    {
         void WriteLog(string logJson);

         void SelectLog(string filter);
    }
}
