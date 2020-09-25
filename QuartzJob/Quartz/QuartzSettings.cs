namespace QuartzJob.Quartz
{
    public class QuartzSettings
    {
        public string SchedulerInstanceName { get; set; }

        public string SchedulerInstanceId { get; set; }

        public string ThreadPoolType { get; set; }

        public string ThreadPoolThreadCount { get; set; }

        public string JobStoreType { get; set; }

        public string JobStoreClustered { get; set; }

        public string JobStoreMisfireThreshold { get; set; }

        public string JobStoreClusterCheckinInterval { get; set; }

        public string JobStoreTablePrefix { get; set; }

        public string JobStoreDriverDelegateType { get; set; }

        public string JobStoreUseProperties { get; set; }

        public string SerializerType { get; set; }

        public string JobStoreDataSource { get; set; }

        public string DataSourceSqlserverDsProvider { get; set; }

        public string DataSourceSqlserverDsConnectionString { get; set; }
    }
}