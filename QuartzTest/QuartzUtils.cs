using Quartz;
using Quartz.Impl;
using QuartzTest.JobFactories;
using QuartzTest.JobParameters;
using QuartzTest.Jobs;
using System.Collections.Specialized;
using System.Reflection.Metadata;

namespace QuartzTest
{
    public static class QuartzUtils
    {
        private const int IntervalSeconds = 3;
        private const int RepeatCount = 3;
        public static async Task<IScheduler> ConfigureQuartz()
        {
            Console.WriteLine("Creating scheduler...");
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };

            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            var scheduler = factory.GetScheduler().Result;
            await scheduler.Start();

            return scheduler;
        }

        public static async Task ScheduleSimpleJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .UsingJobData("username", "richard")
                .UsingJobData("password", "medina")
                .WithIdentity("simplejob", "qartzsamples")
                .Build();

            job.JobDataMap.Put("simplejobparameters", new SimpleJobParameter
            {
                Username = "richard2",
                Password = "password2"
            });

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("simpletrigger", "qartzsamples")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalSeconds).WithRepeatCount(RepeatCount))
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public static async Task ScheduleSimpleJobDurably(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .UsingJobData("username", "richard")
                .UsingJobData("password", "medina")
                .WithIdentity("simplejob", "qartzsamples")
                .StoreDurably()
                .Build();

            job.JobDataMap.Put("simplejobparameters", new SimpleJobParameter
            {
                Username = "richard2",
                Password = "password2"
            });

            await scheduler.AddJob(job, true);

            ITrigger trigger = TriggerBuilder.Create()
                .ForJob("simplejob", "qartzsamples")
                .WithIdentity("simpletrigger", "qartzsamples")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalSeconds).WithRepeatCount(RepeatCount))
                .Build();

            await scheduler.ScheduleJob(trigger);
        }

        public static async Task ScheduleSimpleJobDurablyTriggerParameters(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .UsingJobData("username", "richard")
                .UsingJobData("password", "medina")
                .WithIdentity("simplejob", "qartzsamples")
                .StoreDurably()
                .Build();

            job.JobDataMap.Put("simplejobparameters", new SimpleJobParameter
            {
                Username = "richard2",
                Password = "password2"
            });

            await scheduler.AddJob(job, true);

            ITrigger trigger = TriggerBuilder.Create()
                .ForJob("simplejob", "qartzsamples")
                .WithIdentity("simpletrigger", "qartzsamples")
                .UsingJobData("triggerparam", "This is Trigger 1")
                .UsingJobData("username", "kristian")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalSeconds).WithRepeatCount(RepeatCount))
                .Build();

            await scheduler.ScheduleJob(trigger);
        }

        public static async Task ScheduleSimpleJobDurablyTriggerParametersMultiple(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .UsingJobData("username", "richard")
                .UsingJobData("password", "medina")
                .WithIdentity("simplejob", "qartzsamples")
                .StoreDurably()
                .Build();

            job.JobDataMap.Put("simplejobparameters", new SimpleJobParameter
            {
                Username = "richard2",
                Password = "password2"
            });

            await scheduler.AddJob(job, true);

            ITrigger trigger = TriggerBuilder.Create()
                .ForJob("simplejob", "qartzsamples")
                .WithIdentity("simpletrigger", "qartzsamples")
                .UsingJobData("triggerparam", "This is Trigger 1")
                .UsingJobData("username", "kristian")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalSeconds).WithRepeatCount(RepeatCount))
                .Build();

            await scheduler.ScheduleJob(trigger);

            ITrigger trigger2 = TriggerBuilder.Create()
                .ForJob("simplejob", "qartzsamples")
                .WithIdentity("simpletrigger2", "qartzsamples")
                .UsingJobData("triggerparam", "This is Trigger 2")
                .UsingJobData("username", "kristian")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalSeconds).WithRepeatCount(RepeatCount))
                .Build();

            await scheduler.ScheduleJob(trigger2);
        }

        public static async Task ScheduleSimpleJobDurablyForever(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .UsingJobData("username", "richard")
                .UsingJobData("password", "medina")
                .WithIdentity("simplejob", "qartzsamples")
                .StoreDurably()
                .Build();

            job.JobDataMap.Put("simplejobparameters", new SimpleJobParameter
            {
                Username = "richard2",
                Password = "password2"
            });

            await scheduler.AddJob(job, true);

            ITrigger trigger = TriggerBuilder.Create()
                .ForJob("simplejob", "qartzsamples")
                .WithIdentity("simpletrigger", "qartzsamples")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalSeconds).RepeatForever())
                
                .Build();

            await scheduler.ScheduleJob(trigger);
        }

        public static async Task ScheduleSimpleJobDailyTimeSchedule(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .UsingJobData("username", "richard")
                .UsingJobData("password", "medina")
                .WithIdentity("simplejob", "qartzsamples")
                .StoreDurably()
                .Build();

            job.JobDataMap.Put("simplejobparameters", new SimpleJobParameter
            {
                Username = "richard2",
                Password = "password2"
            });

            await scheduler.AddJob(job, true);

            ITrigger trigger = TriggerBuilder.Create()
                .ForJob("simplejob", "qartzsamples")
                .WithIdentity("simpletrigger", "qartzsamples")
                .StartNow()
                .WithDailyTimeIntervalSchedule(x => x
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(21,0))
                    .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(21,10))
                    .WithIntervalInSeconds(IntervalSeconds)
                    .WithRepeatCount(RepeatCount)
                    .OnDaysOfTheWeek(DayOfWeek.Monday, DayOfWeek.Wednesday)
                 )
                .Build();

            await scheduler.ScheduleJob(trigger);
        }

        public static async Task ScheduleSimpleJobCalendarIntervalSchedule(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .UsingJobData("username", "richard")
                .UsingJobData("password", "medina")
                .WithIdentity("simplejob", "qartzsamples")
                .StoreDurably()
                .Build();

            job.JobDataMap.Put("simplejobparameters", new SimpleJobParameter
            {
                Username = "richard2",
                Password = "password2"
            });

            await scheduler.AddJob(job, true);

            ITrigger trigger = TriggerBuilder.Create()
                .ForJob("simplejob", "qartzsamples")
                .WithIdentity("simpletrigger", "qartzsamples")
                .StartNow()
                .WithCalendarIntervalSchedule(x => x
                    .WithIntervalInDays(1)
                    .PreserveHourOfDayAcrossDaylightSavings(true)
                    .SkipDayIfHourDoesNotExist(true)
                )
                .Build();

            await scheduler.ScheduleJob(trigger);
        }

        public static async Task ScheduleIocJob(IScheduler scheduler, IServiceProvider serviceProvider)
        {
            scheduler.JobFactory = new IocJobFactory(serviceProvider);

            //IJobDetail job = JobBuilder.Create<IocJob>()
            //    .WithIdentity("iocjob", "qartzsamples")
            //    .StoreDurably()
            //    .Build();

            //await scheduler.AddJob(job, true);

            //ITrigger trigger = TriggerBuilder.Create()
            //    .ForJob("iocjob", "qartzsamples")
            //    .WithIdentity("simpletrigger", "qartzsamples")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalSeconds).WithRepeatCount(RepeatCount))
            //    .Build();

            //await scheduler.ScheduleJob(trigger);
        }
    }
}
