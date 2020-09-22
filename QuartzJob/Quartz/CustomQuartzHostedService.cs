using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace QuartzJob.Quartz
{
    public class CustomQuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobMetadata> _jobsMetadata;
        private readonly IConfiguration _configuration;

        public CustomQuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IConfiguration configuration, IEnumerable<JobMetadata> jobsMetadata)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _configuration = configuration;
            _jobsMetadata = jobsMetadata;
        }

        private IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var settings = _configuration.GetSection("QuartzSettings").Get<QuartzSettings>();

                Scheduler = await new StdSchedulerFactory(settings.ToProperties()).GetScheduler(cancellationToken);

                // Scheduler = await _schedulerFactory.GetScheduler(cancellationToken); 

                Scheduler.JobFactory = _jobFactory;

                foreach (var jobMetadata in _jobsMetadata)
                {
                    var job = CreateJob(jobMetadata);
                    var trigger = CreateTrigger(jobMetadata, settings.TimeZone);

                    if (!await Scheduler.CheckExists(job.Key, cancellationToken))
                    {
                        await Scheduler.ScheduleJob(job, trigger, cancellationToken);
                    }
                }

                await Scheduler.Start(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(JobMetadata schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName!)
                .WithDescription(jobType.Name)
                .RequestRecovery()
                .Build();
        }

        private static ITrigger CreateTrigger(JobMetadata schedule, string timeZone)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression,
                    x => x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZone)))
                .WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}