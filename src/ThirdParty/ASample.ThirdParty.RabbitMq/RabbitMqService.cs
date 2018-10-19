using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ASample.ThirdParty.RabbitMq
{
    /// <summary>
    /// RabbitMq服务类
    /// </summary>
    public class RabbitMqService
    {
        private  static IConnection Connection { get; set; }

        static RabbitMqService()
        {
            Connection = GetConnection();
        }
        public static IConnection GetConnection()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.Port = 5672;
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            return factory.CreateConnection();
        }

        public bool SendMessage(string message, string friendqueue)
        {
            try
            {
                IModel channel = Connection.CreateModel();
                channel.ExchangeDeclare("messageexchange", ExchangeType.Direct);
                channel.QueueDeclare(friendqueue, true, false, false, null);
                channel.QueueBind(friendqueue, "messageexchange", friendqueue, null);
                var msg = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("messageexchange", friendqueue, null, msg);

            }
            catch (Exception)
            {


            }
            return true;

        }
        public string ReceiveMessage(string myqueue)
        {
            try
            {
                string queue = myqueue;
                IModel channel = Connection.CreateModel();
                channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                BasicGetResult result = channel.BasicGet(queue: queue, autoAck: true);
                if (result != null)
                    return Encoding.UTF8.GetString(result.Body);
                else
                    return null;
            }
            catch (Exception)
            {
                return null;

            }

        }
    }
}
