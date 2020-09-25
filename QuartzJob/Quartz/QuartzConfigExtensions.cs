using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzJob.Job;

namespace QuartzJob.Quartz
{
    public static class QuartzConfigExtensions
    {
        public static void AddCustomQuartz(this IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSingleton<IJobFactory, CustomQuartzJobFactory>();
            
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            
            services.AddSingleton<NotificationJob>();
            
            services.AddSingleton(
                new JobMetadata(
                    Guid.NewGuid(),
                    typeof(NotificationJob),
                    "Notification Job",
                    "0 0/1 * 1/1 * ? *")); // Execute job every 1 minute
            
            // services.AddSingleton(
            //     new JobMetadata(
            //         Guid.NewGuid(),
            //         typeof(NotificationJob),
            //         "Notification Job",
            //         "0 1 11 1/1 * ? *")); // Execute job at 11:01 am everyday
            
            services.AddHostedService<CustomQuartzHostedService>();
        }
    }
}