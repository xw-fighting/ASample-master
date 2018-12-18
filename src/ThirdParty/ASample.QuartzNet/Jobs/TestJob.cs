using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.QuartzNet.Jobs
{
    public class TestJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "Job_Record.txt";
            Logger.LogFileInfo.WriteLogInfo("测试任务开始执行", path);
            Console.WriteLine("执行了");
            HttpClient client = new HttpClient();
            client.PostAsync("https://www.baidu.com", null);
            return null;
        }
    }
}
