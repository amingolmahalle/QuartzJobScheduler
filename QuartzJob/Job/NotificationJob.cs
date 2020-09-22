using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace QuartzJob.Job
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;

        public NotificationJob(ILogger<NotificationJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Execute Job at {DateTime.Now}");

            return Task.CompletedTask;
        }
    }
}