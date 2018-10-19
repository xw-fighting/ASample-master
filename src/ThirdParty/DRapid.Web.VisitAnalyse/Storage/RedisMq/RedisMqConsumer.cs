using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DRapid.Utility.Reflection;
using DRapid.Utility.Threading.Tasks;
using DRapid.Web.VisitAnalyse.Core;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DRapid.Web.VisitAnalyse.Storage.RedisMq
{
    public class RedisMqConsumer
    {
        public RedisMqConsumer(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public RedisMqConsumer(IOptions<RedisMqConsumerOptions> options)
        {
            Options = options.Value;
            var connection = ConnectionMultiplexer.Connect(Options.ConnectionString);
            Subscriber = connection.GetSubscriber();
        }

        public RedisMqConsumerOptions Options { get; set; }

        protected ISubscriber Subscriber { get; set; }

        public async Task RegisterHandlerAsync(IEnumerable<object> handlers)
        {
            foreach (var handler in handlers)
            {
                await RegisterHandlerAsync(handler);
            }
        }

        private async Task RegisterHandlerAsync(object handler)
        {
            var handlerType = handler.GetType();
            var interfs = handler.GetType().GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRedisMqConsumerHandler<>));
            foreach (var interf in interfs)
            {
                var methodDef = interf.GetMethods().Single();
                var method = ReflectionActions.GetImplementOfInterfaceMethod(handlerType, methodDef);
                var type = interf.GetGenericArguments().Single();
                await Subscriber.SubscribeAsync(type, (item, channel, message) =>
                {
                    method.Invoke(handler, new[] {item, channel, message});
                    return DoneTask.Done;
                });
            }
        }
    }
}