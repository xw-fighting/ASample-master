using ASample.SendEmail.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.SendEmail.Test
{
    [TestClass]
    public class SendEamilTest
    {
        [TestMethod]
        public void SendEmailTest()
        {
            //默认配置
            EamilMessage eamilMessage1 = new EamilMessage();
            eamilMessage1.DisplayName = "你好";//这个值可以全局配置，也可以在发送人里面动态配置，比如相同发送人，给不同收件人发送消息
                                              //优先级 发送消息的显示名称 -》 发送人的显示名称
            eamilMessage1.Subject = "测试标题";//必填
            eamilMessage1.EmailBody = "测试内容";//必填
            eamilMessage1.ToMail = "542235197@qq.com";//必填
            EmailSenderService emailProvider = new EmailSenderService(eamilMessage1);
            emailProvider.Send();

            ////2,动态设置不同的发送人
            //Model.EmailParam maitSender = new Model.EmailParam();
            //maitSender.DisplayName = "布兰德";
            //maitSender.FromMail = "18079605966@163.com";
            //maitSender.MSenderUsername = "18079605966@163.com";
            //maitSender.MSenderPassword = "";
            ////MaitSender 还可以动态配置其他参数

            //EamilMessage eamilMessage2 = new EamilMessage();
            //eamilMessage2.DisplayName = "你好";
            //eamilMessage2.Subject = "测试标题";
            //eamilMessage2.EmailBody = "测试内容";
            //eamilMessage2.ToMail = "18079605966@163.com";
            //emailProvider.SetEmail(eamilMessage2, maitSender);
            //emailProvider.Send();

            ////3，全局配置
            //EamilDefaultConfig.Init("18079605966@163.com", "smtp.163.com", "18079605966@163.com", "", "你好");

            //EamilMessage eamilMessage3 = new EamilMessage();
            //eamilMessage3.DisplayName = "你好";
            //eamilMessage3.Subject = "测试标题";
            //eamilMessage3.EmailBody = "测试内容";
            //eamilMessage3.ToMail = "18079605966@163.com";
            //emailProvider.SetEmail(eamilMessage3);
            //emailProvider.Send();
        }
    }
}
