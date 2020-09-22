using System.Collections.Specialized;

namespace QuartzJob.Quartz
{
    public static class QuartzSettingsExtensions
    {
        public static NameValueCollection ToProperties(this QuartzSettings settings)
        {
            var props = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", settings.SchedulerInstanceName},
                { "quartz.scheduler.instanceId", settings.SchedulerInstanceId},
                { "quartz.threadPool.type", settings.ThreadPoolType},
                { "quartz.threadPool.threadCount", settings.ThreadPoolThreadCount},
                { "quartz.jobStore.type", settings.JobStoreType},
                { "quartz.jobStore.clustered", settings.JobStoreClustered},
                { "quartz.jobStore.misfireThreshold", settings.JobStoreMisfireThreshold},
                { "quartz.jobStore.clusterCheckinInterval", settings.JobStoreClusterCheckinInterval},
                { "quartz.jobStore.tablePrefix", settings.JobStoreTablePrefix},
                { "quartz.jobStore.driverDelegateType", settings.JobStoreDriverDelegateType},
                { "quartz.jobStore.useProperties", settings.JobStoreUseProperties},
                { "quartz.serializer.type", settings.SerializerType },
                { "quartz.jobStore.dataSource", settings.JobStoreDataSource},
                { "quartz.dataSource.sqlserverDS.provider", settings.DataSourceSqlserverDsProvider},
                { "quartz.dataSource.sqlserverDS.connectionString", settings.DataSourceSqlserverDsConnectionString}
            };
            return props;
        }
    }
}