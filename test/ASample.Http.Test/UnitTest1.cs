using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASample.Http.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var result =  HttpWraper.GetAsync("https://www.baidu.com").GetAwaiter().GetResult();
            Trace.Write( result);
        }
    }
}
