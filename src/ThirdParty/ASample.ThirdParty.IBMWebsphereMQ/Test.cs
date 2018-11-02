using IBM.XMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASample.ThirdParty.IBMWebsphereMQ
{
    public class TestLisnter
    {
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
            service.LogInfo(textMsg.ToString(), "IBM_MessageLister_Log", ".txt");
            //Console.WriteLine(msg);
        }

        public void Test1()
        {
            //XMSFactoryFactory factoryFactory = XMSFactoryFactory.GetInstance(XMSC.WPM_CP_TCP);

            //IConnectionFactory connectionFactory = factoryFactory.CreateConnectionFactory();
            //connectionFactory.SetStringProperty(XMSC.RTT_HOST_NAME, "localhost");
            //connectionFactory.SetStringProperty(XMSC.RTT_PORT, "1415");
            //connectionFactory.SetStringProperty(XMSC.WMQ_CHANNEL, "TEST.CHANEL");
            //connectionFactory.SetStringProperty(XMSC.WMQ_QUEUE_MANAGER, "TEST");

            //connectionFactory.SetIntProperty(XMSC.WMQ_CONNECTION_MODE, XMSC.WMQ_CM_CLIENT_UNMANAGED);
            //connectionFactory.SetIntProperty(XMSC.WMQ_BROKER_VERSION, XMSC.WMQ_BROKER_V1);
            //connectionFactory.SetStringProperty(XMSC.CLIENT_ID, string.Empty);

            //var factoryFactory = XMSFactoryFactory.GetInstance(XMSC.CT_WMQ);
            //var connectionFactory = factoryFactory.CreateConnectionFactory();

            //connectionFactory.SetStringProperty(XMSC.WMQ_HOST_NAME, "localhost");
            //connectionFactory.SetIntProperty(XMSC.WMQ_PORT, 1415);
            //connectionFactory.SetStringProperty(XMSC.WMQ_QUEUE_MANAGER, "TEST");
            //connectionFactory.SetIntProperty(XMSC.WMQ_CONNECTION_MODE, XMSC.WMQ_CM_CLIENT);
            //connectionFactory.SetIntProperty(XMSC.WMQ_BROKER_VERSION, XMSC.WMQ_BROKER_V1);
            //connectionFactory.SetStringProperty(XMSC.CLIENT_ID, string.Empty);

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
            //
            // Create the connection and register an exception listener
            //

            //IConnection connection = connectionFactory.CreateConnection();
            ////connection.ExceptionListener = new ExceptionListener(Sample.OnException);

            //ISession session = connection.CreateSession(false, AcknowledgeMode.AutoAcknowledge);
            //IDestination topic = session.CreateTopic("topic://xms/sample");

            ////
            //// Create the consumer and register an async message listener
            ////

            //IMessageConsumer consumer = session.CreateConsumer(topic);
            //consumer.MessageListener = new MessageListener(OnMessage);

            //connection.Start();

            //while (true)
            //{
            //    Console.WriteLine("Waiting for messages....");
            //    Thread.Sleep(1000);
            //}
        }
    }
}
