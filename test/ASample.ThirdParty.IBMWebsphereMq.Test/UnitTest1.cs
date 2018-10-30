using System;
using ASample.ThirdParty.IBMWebsphereMQ;
using ASample.ThirdParty.IBMWebsphereMQ.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASample.ThirdParty.IBMWebsphereMq.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConnecting()
        {
            var service = new IBMWebspherMqService(new MqConstant {
                HostName = "127.0.0.1",
                Port = 1414,
                //ManagerName = "",
                ChannelName = "xw_testC",
                QueueName = "One",
                QueueManager = "xw_test"
            });

            var result = service.MqConnecting();
        }

        
    }


}
