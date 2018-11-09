using ASample.ThirdParty.IBMWebsphereMQ.Model;
using IBM.WMQ;
using IBM.WMQ.PCF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASample.ThirdParty.IBMWebsphereMQ
{
    public class IBMWebspherMqService
    {
        private static IBMWebspherMqService _Singleton = null;
        private static object _Lock = new object();
        public static MqConstant MqConstant { get; set; }
        public static IBMWebspherMqService CreateInstance(MqConstant wmqConstant)
        {
            if (_Singleton == null) //
            {
                lock (_Lock)
                {

                    if (_Singleton == null)
                    {
                        _Singleton = new IBMWebspherMqService();
                        MqConstant = wmqConstant;
                    }
                }
            }
            return _Singleton;
        }

        private MQQueueManager qMgr;
        private static string filebasepath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";

        public string MqConnecting()
        {
            var errMsg = string.Empty;
            if(!string.IsNullOrWhiteSpace(MqConstant.MQCCSID))
            {
                Environment.SetEnvironmentVariable("MQCCSID", MqConstant.MQCCSID);
                if (MQEnvironment.properties.Count <= 0)
                {
                    MQEnvironment.properties.Add(MQC.CCSID_PROPERTY, MqConstant.MQCCSID);
                }
            }
            MQEnvironment.Port = MqConstant.Port;
            MQEnvironment.Channel = MqConstant.ChannelName;
            MQEnvironment.Hostname = MqConstant.HostName;
            MQEnvironment.UserId = MqConstant.UserId;
            MQEnvironment.Password = MqConstant.Password;
            try
            {
                if (qMgr == null || !qMgr.IsConnected)
                {
                    qMgr = new MQQueueManager(MqConstant.QueueManager);
                }
                errMsg = "连接队列管理器:" + "成功！";
            }
            catch (MQException e)
            {
                errMsg = "连接队列管理器错误: 结束码：" + e.CompletionCode + " 错误原因代码：" + e.ReasonCode;
            }
            catch (Exception e)
            {
                errMsg = "连接队列管理器错误: 结束码：" + e;
            }
            return errMsg;
        }

        public bool MqConnecting(out string errMsg)
        {
            errMsg = string.Empty;
            if (!string.IsNullOrWhiteSpace(MqConstant.MQCCSID))
            {
                Environment.SetEnvironmentVariable("MQCCSID", MqConstant.MQCCSID);
                if (MQEnvironment.properties.Count <= 0)
                {
                    MQEnvironment.properties.Add(MQC.CCSID_PROPERTY, MqConstant.MQCCSID);
                }
            }
            MQEnvironment.Port = MqConstant.Port;
            MQEnvironment.Channel = MqConstant.ChannelName;
            MQEnvironment.Hostname = MqConstant.HostName;
            MQEnvironment.UserId = MqConstant.UserId;
            MQEnvironment.Password = MqConstant.Password;
            string qmName = MqConstant.QueueManager;
            try
            {
                if (qMgr == null || !qMgr.IsConnected)
                {
                    qMgr = new MQQueueManager(qmName);
                }
                errMsg = "连接队列管理器:" + "成功！";
                return true;
            }
            catch (MQException e)
            {
                errMsg = "连接队列管理器错误: 结束码：" + e.CompletionCode + " 错误原因代码：" + e.ReasonCode;
                return false;
            }
            catch (Exception e)
            {
                errMsg = "连接队列管理器错误: 结束码：" + e;
                return false;
            }
            
        }

        //public delegate void MessageListener(IMessage msg);

       

        /// <summary>
        /// 读取消息
        /// </summary>
        /// <returns></returns>
        public ReturnResult ReadMessage()
        {
            try
            {
                if (!MqConnecting(out string errMsg))
                {
                    LogInfo(errMsg, "IBM_Error_Log", ".txt");
                    return ReturnResult.Error(errMsg);
                }
                var queue = qMgr.AccessQueue(MqConstant.QueueName, MQC.MQOO_INPUT_AS_Q_DEF | MQC.MQOO_FAIL_IF_QUIESCING);
                var gmo = new MQGetMessageOptions
                {
                    Options = MQC.MQGMO_WAIT,
                    WaitInterval = 1000,
                    MatchOptions = MQC.MQMO_NONE
                };
                var message = new MQMessage();
                queue.Get(message);
                var messageReturn = message.ReadString(message.MessageLength);
                return ReturnResult.Success(messageReturn);
            }
            catch (Exception ex)
            {
                LogInfo(ex.ToString() + "\n" + ex.Message, "IBM_Error_Log", ".txt");
                return ReturnResult.Error(ex.Message);
            }
        }
        /// <summary>
        /// 写入消息
        /// </summary>
        public ReturnResult WriteMessage( string body)
        {
            if (!MqConnecting(out string errMsg))
            {
                LogInfo(errMsg, "IBM_Error_Log", ".txt");
                return ReturnResult.Error(errMsg);
            }
            try
            {
                var queue = qMgr.AccessQueue(MqConstant.QueueName, MQC.MQOO_OUTPUT);
                var message = new MQMessage();
                message.WriteString(body);
                message.Format = MQC.MQFMT_STRING;
                queue.Put(message);
                return ReturnResult.Success("数据插入成功");
            }
            catch (Exception ex)
            {
                LogInfo(ex.ToString() + "\n" + ex.Message, "IBM_Error_Log", ".txt");
                return ReturnResult.Error(ex.Message );
            }
        }

        /// <summary>
        /// 创建本地队列
        /// </summary>
        /// <param name="qmName">队列管理器</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="isTransmissionQueue">是否为传输队列</param>
        public void CreateQueue(string qmName, string queueName, bool isTransmissionQueue = false)
        {
            PCFMessageAgent agent = new PCFMessageAgent(qmName);
            PCFMessage request = new PCFMessage(CMQCFC.MQCMD_CREATE_Q);
            request.AddParameter(MQC.MQCA_Q_NAME, queueName);
            request.AddParameter(MQC.MQIA_Q_TYPE, MQC.MQQT_LOCAL);
            if (isTransmissionQueue) request.AddParameter(MQC.MQIA_USAGE, MQC.MQUS_TRANSMISSION);
            PCFMessage[] response = agent.Send(request);
        }

        /// <summary>
        /// 删除队列
        /// </summary>
        public void DelQueue(string qmName, string queueName)
        {
            PCFMessageAgent agent = new PCFMessageAgent(qmName);
            PCFMessage request = new PCFMessage(CMQCFC.MQCMD_DELETE_Q);
            request.AddParameter(MQC.MQCA_Q_NAME, queueName);
            PCFMessage[] response = agent.Send(request);
        }
       
        /// <summary>
        /// 获取当前管理器所有队列
        /// </summary>
        /// <param name="qmName">队列管理器</param>
        /// <param name="isFindSystemQueue">是否包含系统队列</param>
        /// <returns></returns>
        public List<string> GetALLQueue(string qmName, bool isFindSystemQueue = false)
        {
            PCFMessageAgent agent = new PCFMessageAgent(qmName);
            PCFMessage request = new PCFMessage(CMQCFC.MQCMD_INQUIRE_Q_NAMES);
            request.AddParameter(MQC.MQCA_Q_NAME, "*");
            PCFMessage[] response = agent.Send(request);
            string[] names = response[0].GetStringListParameterValue(CMQCFC.MQCACF_Q_NAMES);
            List<string> result = null;
            if (!isFindSystemQueue)
                result = names.ToList().Where(s => !s.Contains("AMQ.") && !s.Contains("SYSTEM.")).ToList();
            else
                result = names.ToList();
            return result;
        }

        /// <summary>
        /// 创建远程队列
        /// </summary>
        /// <param name="qmName">队列管理器</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="ycqmName">远程队列管理器名称</param>
        /// <param name="ycqueueName">远程队列名称</param>
        /// <param name="csqueueName">传输队列名称</param>
        public void CreateRemoteQueue(string qmName, string queueName, string ycqmName, string ycqueueName, string csqueueName)
        {
            PCFMessageAgent agent = new PCFMessageAgent(qmName);
            PCFMessage request = new PCFMessage(CMQCFC.MQCMD_CREATE_Q);
            request.AddParameter(MQC.MQCA_Q_NAME, queueName);
            request.AddParameter(MQC.MQIA_Q_TYPE, MQC.MQQT_REMOTE);
            request.AddParameter(MQC.MQCA_REMOTE_Q_MGR_NAME, ycqmName);
            request.AddParameter(MQC.MQCA_REMOTE_Q_NAME, ycqueueName);
            request.AddParameter(MQC.MQCA_XMIT_Q_NAME, csqueueName);
            PCFMessage[] response = agent.Send(request);
            agent.Disconnect();
        }
        
        /// <summary>
        /// 创建发送方通道
        /// </summary>
        /// <param name="qmName">队列管理器</param>
        /// <param name="channelName">通道名称</param>
        /// <param name="transmissionQueueName">传输队列名称</param>
        /// <param name="iPPort">IP地址与端口号 例：localhost(1415) </param>
        public void CreateChannelBySend(string qmName, string channelName, string transmissionQueueName, string iPPort)
        {
            PCFMessageAgent agent = new PCFMessageAgent(qmName);
            PCFMessage request = new PCFMessage(CMQCFC.MQCMD_CREATE_CHANNEL);
            request.AddParameter(CMQCFC.MQCACH_CHANNEL_NAME, channelName);

            request.AddParameter(CMQCFC.MQIACH_CHANNEL_TYPE, MQC.MQCHT_SENDER);
            request.AddParameter(CMQCFC.MQCACH_CONNECTION_NAME, iPPort);
            request.AddParameter(CMQCFC.MQCACH_XMIT_Q_NAME, transmissionQueueName);

            PCFMessage[] response = agent.Send(request);
            agent.Disconnect();
        }
        
        /// <summary>
        /// 创建接受方通道
        /// </summary>
        /// <param name="qmName">队列管理器</param>
        /// <param name="channelName">管道名称</param>
        public void CreateChannelByReceive(string qmName, string channelName)
        {
            PCFMessageAgent agent = new PCFMessageAgent(qmName);
            PCFMessage request = new PCFMessage(CMQCFC.MQCMD_CREATE_CHANNEL);
            request.AddParameter(CMQCFC.MQCACH_CHANNEL_NAME, channelName);

            request.AddParameter(CMQCFC.MQIACH_CHANNEL_TYPE, MQC.MQCHT_RECEIVER);

            PCFMessage[] response = agent.Send(request);
            agent.Disconnect();
        }

        /// <summary>
        /// 记录信息到文件
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="filename">文件名</param>
        /// <param name="filefix">后缀名</param>
        public  void LogInfo(string info, string filename, string filefix)
        {
            try
            {
                string _filebasepath = "Logs\\" + GetTodayRecordPath();
                string filePath = String.Empty;
                //日志文件路径
                if (string.IsNullOrEmpty(filefix))
                {
                    filePath = _filebasepath + "\\" + filename + ".log";
                }
                else
                {
                    filePath = _filebasepath + "\\" + filename + filefix;
                }
                if (!System.IO.Directory.Exists(_filebasepath))
                {
                    Directory.CreateDirectory(_filebasepath);
                }
                if (!File.Exists(filePath))//如果文件不存在，则创建该文件
                {
                    File.Create(filePath).Close();
                }
                //StreamWriter sw = File.AppendText(filePath);
                StreamWriter sw = File.AppendText(filePath);
                sw.Write("\r\n"+info);
                sw.Close();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 获取当前日期文件夹
        /// </summary>
        /// <returns></returns>
        private string GetTodayRecordPath()
        {
            string createPath = string.Empty;
            if (Directory.Exists(filebasepath + DateTime.Now.Year.ToString()) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(filebasepath + DateTime.Now.Year.ToString());
                createPath = DateTime.Now.Year.ToString() + @"\";
            }
            if (Directory.Exists(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString()) == false)
            {
                Directory.CreateDirectory(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString());
                createPath = DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\";
            }
            if (Directory.Exists(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("yyyyMMdd")) == false)
            {
                Directory.CreateDirectory(filebasepath + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.ToString("dd"));
                createPath = DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.ToString("dd") + "/";
            }
            createPath = DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.ToString("dd") + @"\";
            return createPath;
        }

    }
}
