using ASample.ThirdParty.IBMWebsphereMQ.Model;
using IBM.XMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.IBMWebsphereMQ.Core
{
    public class IBMWMQMMsgLister
    {
        public static MqConstant MqConstant;
        private static IBMWMQMMsgLister _Singleton = null;
        private static object _Lock = new object();
        private static IConnection conn = null;
        public static IBMWMQMMsgLister CreateInstance(MqConstant wmqConstant)
        {
            if (_Singleton == null) //
            {
                lock (_Lock)
                {

                    if (_Singleton == null)
                    {
                        _Singleton = new IBMWMQMMsgLister();
                        MqConstant = wmqConstant;
                    }
                }
            }
            return _Singleton;
        }

        public void MessageLister()
         {
            var xff = XMSFactoryFactory.GetInstance(XMSC.CT_WMQ);
            var cf = xff.CreateConnectionFactory();
            cf.SetStringProperty(XMSC.WMQ_HOST_NAME, MqConstant.HostName);
            cf.SetIntProperty(XMSC.WMQ_PORT, MqConstant.Port);
            cf.SetStringProperty(XMSC.WMQ_CHANNEL, MqConstant.ChannelName);
            cf.SetIntProperty(XMSC.WMQ_CONNECTION_MODE, XMSC.WMQ_CM_CLIENT_UNMANAGED);
            cf.SetStringProperty(XMSC.WMQ_QUEUE_MANAGER, MqConstant.QueueManager);
            cf.SetIntProperty(XMSC.WMQ_BROKER_VERSION, XMSC.WMQ_BROKER_V1);

            conn = cf.CreateConnection(MqConstant.UserId, MqConstant.Password);
            Console.WriteLine("connection created");
            var sess = conn.CreateSession(false, AcknowledgeMode.AutoAcknowledge);
            var dest = sess.CreateQueue(MqConstant.QueueName);
            var consumer = sess.CreateConsumer(dest);
            MessageListener ml = new MessageListener(OnMessage);
            consumer.MessageListener = ml;
            conn.Start();
            Console.WriteLine("Consumer started");
         }

        public void OnMessage(IMessage msg)
        {
            var service = IBMWebspherMqService.CreateInstance(MqConstant);
            ITextMessage textMsg = (ITextMessage)msg;
            //此时已经从队列中取出
            service.LogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+textMsg.Text, "IBM_Lister_Log", ".txt");
            //Console.WriteLine(msg);
        }

        public void CloseLister()
        {
            conn.Close();
        }
    }
}
