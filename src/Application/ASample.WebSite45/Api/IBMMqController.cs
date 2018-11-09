using ASample.ThirdParty.IBMWebsphereMQ;
using ASample.ThirdParty.IBMWebsphereMQ.Core;
using ASample.ThirdParty.IBMWebsphereMQ.Model;
using ASample.WebSite45.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASample.WebSite45.Api
{
    [RoutePrefix("api/IBMMq")]
    public class IBMMqController : ApiController
    {
        public MqConstant MqConstant { get; set; }
        public IBMWebspherMqService IBMMqService { get; set; }
        public IBMMqController()
        {
            MqConstant = new MqConstant
            {
                HostName = ConfigReader.ReadByKey("HostName"),
                Port = Convert.ToInt32(ConfigReader.ReadByKey("Port")),
                QueueManager = ConfigReader.ReadByKey("QueueManager"),
                ChannelName = ConfigReader.ReadByKey("ChannelName"),
                QueueName = ConfigReader.ReadByKey("QueueName"),
                UserId = ConfigReader.ReadByKey("UserId"),
                Password = ConfigReader.ReadByKey("Password"),
                MQCCSID = ConfigReader.ReadByKey("MQCCSID"),
            };
            IBMMqService = IBMWebspherMqService.CreateInstance(MqConstant);
        }

        /// <summary>
        /// 打开监听器
        /// </summary>
        [HttpPost]
        public void OpenLister()
        {
            var lister = IBMWMQMMsgLister.CreateInstance(MqConstant);
            lister.MessageLister();
        }

        /// <summary>
        /// 关闭监听器
        /// </summary>
        [HttpPost]
        public void CloseLister()
        {
            var lister = IBMWMQMMsgLister.CreateInstance(MqConstant);
            lister.CloseLister();
        }

        /// <summary>
        /// 写消息
        /// </summary>
        [HttpPost]
        public ReturnResult WriteMessage()
        {
            string msg = "你好";
            if (string.IsNullOrEmpty(msg))
            {
                return ReturnResult.Error("传入消息不能为空");
            }
            var result = IBMMqService.WriteMessage(msg);
            return result;
        }

        /// <summary>
        /// 读消息
        /// </summary>
        [HttpPost]
        public ReturnResult ReadMessage()
        {
            var result = IBMMqService.ReadMessage();
            return result;
        }
    }
}
