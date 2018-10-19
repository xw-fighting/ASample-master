using ASample.Thirdparty.Wechat.Test.Model;
using ASample.ThirdParty.WeChat;
using ASample.ThirdParty.WeChat.WeChatMessageSend.Model.InParam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.Thirdparty.Wechat.Test
{
    [TestClass]
    public class WeChatMessageServiceTest
    {
        [TestMethod]
        public void SendWechatMessageTest()
        {
            var openId = "onru5w7E8hCGPSxAYeOOLHIyw4FM";
            var templateId = "fA4t108_xdmuLMvkyHVZM8MnUdFsH8OYFn_DoozCL2Y";
            var param = new SendMessageTemplateParam
            {
                First = new TemplateDataItem("你好，欢迎光临"),
                Item1 = new TemplateDataItem("你的奖品已经发送，请查收"),
                Item2 = new TemplateDataItem("谢谢你的使用"),
                Remark = new TemplateDataItem("欢迎下次光临")
            };
            var result = WeChatMessageService.SendTemplateMsgAsync(openId, templateId, param);
            var resultStr = JsonConvert.SerializeObject(result.Result);
            Console.WriteLine(resultStr);
        }
    }
}
