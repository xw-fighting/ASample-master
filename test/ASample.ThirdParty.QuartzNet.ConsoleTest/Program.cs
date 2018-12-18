using ASample.ThirdParty.QuartzNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.QuartzNet.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            QuartzNetService.JobsFactory<TestJob>("TestJobDetail", "TestJobTrgger", 20);
            Console.ReadKey();
        }
    }
}
