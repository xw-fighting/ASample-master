using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.IBMWebsphereMQ.Model
{
    public class MqConstant
    {
        /// <summary>
        /// 主机Ip
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get;  set; }

        /// <summary>
        /// Gets the manager name
        /// </summary>
        public string ManagerName { get;  set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get;  set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        public string QueueManager { get; set; }
    }
}
