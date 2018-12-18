using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.QuartzNet
{
    public class QuartzNetService
    {
        public static void JobsFactory<T> (string jobDetailName, string triggerName, int seconds) where T : IJob
        {
            //创建工厂
            ISchedulerFactory factory = new StdSchedulerFactory();
            //计划
            IScheduler scheduler =factory.GetScheduler().GetAwaiter().GetResult();
            scheduler.Start();

            IJobDetail jobDetail = new JobDetailImpl(jobDetailName, null, typeof(T));
            ISimpleTrigger trigger = new SimpleTriggerImpl(triggerName,null,
               DateTime.Now,
               null,
               SimpleTriggerImpl.RepeatIndefinitely,
               TimeSpan.FromSeconds(seconds));

            //执行
            scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
