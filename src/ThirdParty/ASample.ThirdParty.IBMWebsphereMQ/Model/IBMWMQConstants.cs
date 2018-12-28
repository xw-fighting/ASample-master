
namespace ASample.ThirdParty.IBMWebsphereMQ.Model
{
    public class IBMWMQConstants
    {
        /// <summary>
        /// 主机Ip
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 连接接主机的电脑上加入mqm组中的账户名称，默认名称为 MUSR_MQADMIN
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 账户密码，
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 队列管理器名称
        /// </summary>
        public string QueueManager { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string SendQueueName { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string ReceiveQueueName { get; set; }

        /// <summary>
        ///819主要用于unix环境，也可用于unix与windows的通讯
        ///1381主要用于windows环境
        ///1383多用于unix下的中文环境
        ///1386主要用于中文环境的unix与windows的通讯
        /// </summary>
        public string MQCCSID { get; set; }
    }
}
