using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.RabbitMq.Test
{
    /// <summary>
    /// RabbitMq测试方法
    /// </summary>
    [TestClass]
    public class RabbitMqServiceTest
    {
        private static RabbitMqService rabbitMqService = new RabbitMqService();

        [TestMethod]
        public void SendMessageTest()
        {
            var message = "你好吗";
            var friendQuen = "friendQuene";
            var result = rabbitMqService.SendMessage(message, friendQuen);
            Console.WriteLine(result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReceviceMessageTest()
        {
            var friendQuen = "friendQuene";
            var result = rabbitMqService.ReceiveMessage(friendQuen);
            Console.WriteLine(result);
            //Assert.IsTrue(result!=null,"你好");
        }
    }
}
