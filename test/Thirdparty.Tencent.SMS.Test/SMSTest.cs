using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using qcloudsms_csharp;

namespace Thirdparty.Tencent.SMS.Test
{
    [TestClass]
    public class SMSTest
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        [TestMethod]
        public void SendResultTest()
        {
            var result=SmsSend.SendResult("18079652704",new string[] { "456798"},SmsTemplate.Code);

            if (result.result!=0)
            {
                Trace.WriteLine("状态码："+result.result +"\n错误消息："+ result.errMsg);
            }
        }

        /// <summary>
        /// 拉取短信回执内容
        /// 拉取过的内容不会再重复返回，可以理解为消息队列机制
        /// 也就是说 只有一次获取短信回复内容的机会
        /// </summary>
        [TestMethod]
        public void PullReplyTest()
        {
           
            SmsStatusPuller ssPuller = new SmsStatusPuller(1400065422, "");
            SmsStatusPullReplyResult result = ssPuller.pullReply(2);
            if (result.result != 0)
            {
                Trace.WriteLine("状态码：" + result.result + "\n错误消息：" + result.errMsg);
            }
          
        }

        #region MyRegion

        /// <summary>
        /// 拉取短信回执状态
        /// </summary>
        [TestMethod]
        public void PullCallbackTest()
        {
            SmsStatusPuller ssPuller = new SmsStatusPuller(1400065422, "");
            SmsStatusPullCallbackResult result = ssPuller.pullCallback(2);
            if (result.result != 0)
            {
                Trace.WriteLine("状态码：" + result.result + "\n错误消息：" + result.errMsg);
            }
        }
        #endregion

        #region 民办初中信息提交

        /// <summary>
        /// 民办初中信息提交
        /// </summary>
        [TestMethod]
        public void SendMBInfoSubmitTest()
        {
            var result = SmsSend.SendResult("", new string[] { "" }, SmsTemplate.MBInfoSubmit);

            if (result.result != 0)
            {
                Trace.WriteLine("状态码：" + result.result + "\n错误消息：" + result.errMsg);
                Trace.Assert(result.result == 0);
            }
        }
        #endregion
    }
    public class A
    {
        public string B { get; set; }
    }
}
