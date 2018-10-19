using ASample.ThirdParty.WeChat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace ASample.Thirdparty.Wechat
{
    [TestClass]
    public class WeChatAuthManagerTest
    {
        [TestMethod]
        public void GetTokenTest()
        {
            var result = WeChatAuthManager.Current.GetAccessTokenAsync();
            Console.WriteLine(result.Result);
        }

        [TestMethod]
        public void GetBaseUserInfo()
        {
            var openId = "onru5w7E8hCGPSxAYeOOLHIyw4FM";
            var token = "6_z6OVUs5zWXRYqD_PFPj_599JwAzFJq8GNyuhQcDwwqXpYKiNQK7p-HkWHRLslfhsi9pedEcgGK9XjPNM0k4VFuO6INCpjxooFSb64_mNr8TvVI59yEF7QZHQN7MHIKcAFAVWY";
            var result = WeChatAuthManager.Current.GetBasicInfoAsync(openId, token);
            var resultStr = JsonConvert.SerializeObject(result.Result);
            Console.WriteLine(resultStr);
        }
    }
}
