using System;
using ASample.ThirdParty.IBMWebsphereMQ;
using ASample.ThirdParty.IBMWebsphereMQ.Core;
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
            var service = new IBMWebspherMqService(new MqConstant
            {
                HostName = "192.168.1.234",
                Port = 1418,
                //ManagerName = "",
                ChannelName = "CHAN1",
                QueueName = "Q1.DCSI.GOS",
                QueueManager = "QMLJG"
            });

            //var service = new IBMWebspherMqService(new MqConstant
            //{
            //    HostName = "127.0.0.1",
            //    Port = 1415,
            //    //ManagerName = "",
            //    ChannelName = "TEST.CHANEL",
            //    QueueName = "TEST1",
            //    QueueManager = "TEST",
            //    UserId = "ynhx",
            //    Password = "123456"
            //});

            var result = service.MqConnecting();
        }

        [TestMethod]
        public void Test2()
        {
            var service = new IBMWebspherMqService(new MqConstant
            {
                HostName = "localhost",
                Port = 1415,
                ChannelName = "TEST.CHANEL",
                QueueName = "TEST2",
                QueueManager = "TEST"
            });

            var result = service.GetALLQueue("TEST");
        }

        [TestMethod]
        public void TestWriteMessage()
        {
            var service = new IBMWebspherMqService(new MqConstant
            {
                HostName = "192.168.1.234",
                Port = 1418,
                ChannelName = "CHAN1",
                QueueName = "Q1.DCSI.GOS",
                QueueManager = "QMLJG"
            });
            //var service = new IBMWebspherMqService(new MqConstant
            //{
            //    HostName = "127.0.0.1",
            //    Port = 1415,
            //    MQCCSID = "1381",
            //    ChannelName = "TEST.CHANEL",
            //    QueueName = "TEST1",
            //    QueueManager = "TEST",
            //    UserId = "ynhx",
            //    Password = "123456"
            //});
            var jsonStr = "{\"Name\":\"xw\",\"Name2\":\"xw\"}";

            service.WriteMessage(jsonStr);
        }


        [TestMethod]
        public void TestReadMessage()
        {
            var service = new IBMWebspherMqService(new MqConstant
            {
                HostName = "192.168.1.234",
                Port = 1418,
                ChannelName = "CHAN1",
                QueueName = "Q1.DCSI.GOS",
                QueueManager = "QMLJG"
            });
            //var service = new IBMWebspherMqService(new MqConstant
            //{
            //    HostName = "127.0.0.1",
            //    Port = 1415,
            //    //ManagerName = "",
            //    ChannelName = "TEST.CHANEL",
            //    QueueName = "TEST1",
            //    QueueManager = "TEST",
            //    UserId = "ynhx",
            //    Password = "123456"
            //});

            var result = service.ReadMessage();
        }

        [TestMethod]
        public void TestLister()
        {
            var service = new TestLisnter();
            service.Test1();

            var service2 = new IBMWebspherMqService(new MqConstant
            {
                HostName = "192.168.1.234",
                Port = 1418,
                ChannelName = "CHAN1",
                QueueName = "Q1.DCSI.GOS",
                QueueManager = "QMLJG"
            });
            service2.WriteMessage("你好");
        }

        [TestMethod]
        public void TestOpenLister()
        {
            var service = new IBMWMQMMsgLister();
            service.MessageLister();

            //var service2 = new IBMWebspherMqService(new MqConstant
            //{
            //    HostName = "192.168.1.234",
            //    Port = 1418,
            //    ChannelName = "CHAN1",
            //    QueueName = "Q1.DCSI.GOS",
            //    QueueManager = "QMLJG"
            //});
            //service2.WriteMessage("你好");
        }


    }


}
