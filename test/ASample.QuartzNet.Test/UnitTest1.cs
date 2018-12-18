using System;
using ASample.ThirdParty.QuartzNet;
using ASample.ThirdParty.QuartzNet.Jobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASample.QuartzNet.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            QuartzNetService.JobsFactory<TestJob>("TestJobDetail", "TestJobTrigger", 1);
        }
    }
}
