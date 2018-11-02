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
         public void MessageLister()
        {
            XMSFactoryFactory xff = XMSFactoryFactory.GetInstance(XMSC.CT_WMQ);
            IConnectionFactory cf = xff.CreateConnectionFactory();
            cf.SetStringProperty(XMSC.WMQ_HOST_NAME, "192.168.1.234");
            cf.SetIntProperty(XMSC.WMQ_PORT, 1418);
            cf.SetStringProperty(XMSC.WMQ_CHANNEL, "CHAN1");
            cf.SetIntProperty(XMSC.WMQ_CONNECTION_MODE, XMSC.WMQ_CM_CLIENT_UNMANAGED);
            cf.SetStringProperty(XMSC.WMQ_QUEUE_MANAGER, "QMLJG");
            cf.SetIntProperty(XMSC.WMQ_BROKER_VERSION, XMSC.WMQ_BROKER_V1);

            IConnection conn = cf.CreateConnection();
            Console.WriteLine("connection created");
            ISession sess = conn.CreateSession(false, AcknowledgeMode.AutoAcknowledge);
            IDestination dest = sess.CreateQueue("Q1.DCSI.GOS");
            IMessageConsumer consumer = sess.CreateConsumer(dest);
            MessageListener ml = new MessageListener(OnMessage);
            consumer.MessageListener = ml;
            conn.Start();
            Console.WriteLine("Consumer started");
        }

        public void OnMessage(IMessage msg)
        {
            var service = new IBMWebspherMqService(new Model.MqConstant
            {
                HostName = "192.168.1.234",
                Port = 1418,
                ChannelName = "CHAN1",
                QueueName = "Q1.DCSI.GOS",
                QueueManager = "QMLJG"
            });
            ITextMessage textMsg = (ITextMessage)msg;
            service.LogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+textMsg.Text, "IBM_Lister_Log", ".txt");
            //Console.WriteLine(msg);
        }
    }
}
