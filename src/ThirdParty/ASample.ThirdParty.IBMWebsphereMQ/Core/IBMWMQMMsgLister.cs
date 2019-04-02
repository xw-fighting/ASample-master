using IBM.XMS;
using ASample.ThirdParty.IBMWebsphereMQ.Model;

namespace ASample.ThirdParty.IBMWebsphereMQ.Core
{
    public class IBMMqMessageLister
    {
        private static IBMMqMessageLister _Singleton = null;
        private static object _Lock = new object();
        private static IConnection conn = null;

        public static IBMWMQConstants Constant { get; set; }
        public static IBMMqMessageLister CreateInstance(IBMWMQConstants wmqConstant)
        {
            if (_Singleton == null) //
            {
                lock (_Lock)
                {

                    if (_Singleton == null)
                    {
                        _Singleton = new IBMMqMessageLister();
                        Constant = wmqConstant;
                    }
                }
            }
            return _Singleton;
        }
        /// <summary>
        /// 消息监听
        /// </summary>
        public void MessageLister()
        {
            try
            {
                var xff = XMSFactoryFactory.GetInstance(XMSC.CT_WMQ);
                var cf = xff.CreateConnectionFactory();
                cf.SetStringProperty(XMSC.WMQ_HOST_NAME, Constant.HostName);
                cf.SetIntProperty(XMSC.WMQ_PORT, Constant.Port);
                cf.SetStringProperty(XMSC.WMQ_CHANNEL, Constant.ChannelName);
                cf.SetIntProperty(XMSC.WMQ_CONNECTION_MODE, XMSC.WMQ_CM_CLIENT_UNMANAGED);
                cf.SetStringProperty(XMSC.WMQ_QUEUE_MANAGER, Constant.QueueManager);
                cf.SetIntProperty(XMSC.WMQ_BROKER_VERSION, XMSC.WMQ_BROKER_V1);
                cf.SetStringProperty(XMSC.CLIENT_ID, string.Empty);
                conn = cf.CreateConnection();
                //connection created
                var sess = conn.CreateSession(false, AcknowledgeMode.AutoAcknowledge);
                var dest = sess.CreateQueue(Constant.ReceiveQueueName);
                var consumer = sess.CreateConsumer(dest);
                var ml = new MessageListener(OnMessage);
                consumer.MessageListener = ml;
                conn.Start();
                //Consumer started
            }
            catch (System.Exception ex)
            {
                TxtLog.LogInfo(ex.Message, "Lister_Error_Record", ".txt");
                throw;
            }
        }

        public void EndLister()
        {
            conn.Close();
        }

        //监听
        private void OnMessage(IMessage msg)
        {
            var service = IBMWMQService.CreateInstance(Constant);
            ITextMessage textMsg = (ITextMessage)msg;
            var outMsg = textMsg.Text;
            var info = string.Format("出队信息:\r\n消息Id:{0} \r\n 消息文本:{1}", outMsg.Split('$')[0], outMsg.Split('$')[1]);
            TxtLog.LogInfo(info, "OutMsg_Record", ".txt");
            //入库
        }
    }
}
