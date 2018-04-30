using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace NewLetter.jobSchedulers
{
    public class schedulerMain
    {
        public static void Start()
        {
            // For Complete your profile
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<completeYourProfile>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(14, 39))
                  )
                .Build();
            scheduler.ScheduleJob(job, trigger);

            // For Job matching candidate
            IScheduler scheduler2 = StdSchedulerFactory.GetDefaultScheduler();
            scheduler2.Start();
            IJobDetail job2 = JobBuilder.Create<jobMatchingCandidates>().Build();
            ITrigger trigger2 = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(14, 40))
                  )
                .Build();
            scheduler2.ScheduleJob(job2, trigger2);
        }
    }
}