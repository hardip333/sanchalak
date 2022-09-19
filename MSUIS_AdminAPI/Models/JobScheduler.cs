using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSUISApi.Models
{
    public class JobScheduler
    {
        public static void Start()
        {
            //IScheduler scheduler = (IScheduler)StdSchedulerFactory.GetDefaultScheduler();
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<BarcodePDF>().Build();

            ITrigger trigger = TriggerBuilder.Create()
     .WithIdentity("trigger1", "group1")
     .StartNow()
     .WithSimpleSchedule(x => x
         .WithIntervalInMinutes(2)
         .RepeatForever())
     .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}