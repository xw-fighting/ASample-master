using ASample.ThirdParty.IBMWebsphereMQ.Model;
using IBM.WMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.IBMWebsphereMQ
{
    public class IBMWebspherMqService
    {
        public MqConstant MqConstant { get; set; }
        public IBMWebspherMqService(MqConstant mqConstant)
        {
            MqConstant = mqConstant;
        }
        MQQueueManager qMgr;
        MQMessage mqMsg;
        MQQueue queue;
        MQPutMessageOptions putOptions;

        public string MqConnecting()
        {
            var linkStatus = string.Empty;
            string QueueName = MqConstant.QueueName;

            Environment.SetEnvironmentVariable("MQCCSID", "1381");
            if (MQEnvironment.properties.Count <= 0)
            {
                MQEnvironment.properties.Add(MQC.CCSID_PROPERTY, 1381);
            }
            MQEnvironment.Port = MqConstant.Port;
            MQEnvironment.Channel = MqConstant.ChannelName;
            MQEnvironment.Hostname = MqConstant.HostName;
            string qmName = MqConstant.QueueManager;

            try
            {
                if (qMgr == null || !qMgr.IsConnected)
                {
                    qMgr = new MQQueueManager(qmName);
                }

                linkStatus = "连接队列管理器:" + "成功！";
            }
            catch (MQException e)
            {

                linkStatus = "连接队列管理器错误: 结束码：" + e.CompletionCode + " 错误原因代码：" + e.ReasonCode;
            }
            catch (Exception e)
            {

                linkStatus = "连接队列管理器错误: 结束码：" + e;
            }
            return linkStatus;
        }



    }
}
