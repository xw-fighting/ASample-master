using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thirdparty.Tencent.SMS;

namespace ASample.SendEmail.Test
{
    [TestClass]
    public class MessageSenderTest
    {
        [TestMethod]
        public void SendResultTest()
        {
            var result = SmsSend.SendResult("18079652704", new string[] { "456798" }, SmsTemplate.Code);

            if (result.result != 0)
            {
                Trace.WriteLine("状态码：" + result.result + "\n错误消息：" + result.errMsg);
            }
        }
    }
}
