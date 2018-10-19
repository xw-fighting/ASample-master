using ASample.Configuration;
using ASample.ThirdParty.WeChat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ASample.Thirdparty.Wechat.Test
{
    [TestClass]
    public class WeChatCreateMenuServiceTest
    {
        [TestMethod]
        public void CreateMenu()
        {
            var menuJson = ConfigurationReader.Read("menuJson");
            var result = WechatCreateMenuService.CreateMenuAsync(menuJson);
            Console.WriteLine(result);
        }
    }
}
