using System;
using Quartz;
using Quartz.Spi;

namespace QuartzJob.Quartz
{
    public class CustomQuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomQuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle triggerFiredBundle, IScheduler scheduler)
        {
            try
            {
                var jobDetail = triggerFiredBundle.JobDetail;

                return (IJob) _serviceProvider.GetService(jobDetail.JobType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}