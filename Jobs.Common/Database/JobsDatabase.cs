using Jobs.Common.Database.Tables;
using LiteDB;

namespace Jobs.Common.Database
{
    /// <summary>
    /// 
    /// </summary>
    public static class JobsDatabase
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DatabaseName =  "Manager.db";

        /// <summary>
        /// 
        /// </summary>
        public static ILiteCollection<JobGroup> JobGroups { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static ILiteCollection<Job> Jobs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static ILiteCollection<Task> Tasks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static LiteDatabase Database { get; }

        /// <summary>
        /// 
        /// </summary>
        static JobsDatabase()
        {
            Database = new LiteDatabase(DatabaseName);
            BsonMapper.Global.Entity<JobGroup>().DbRef(x => x.ChildGroups, nameof(JobGroups));
            BsonMapper.Global.Entity<JobGroup>().DbRef(x => x.Jobs, nameof(Jobs));
            BsonMapper.Global.Entity<Job>().DbRef(x => x.Tasks, nameof(Tasks));

            JobGroups = Database.GetCollection<JobGroup>(nameof(JobGroups));
            Jobs = Database.GetCollection<Job>(nameof(Jobs));
            Tasks = Database.GetCollection<Task>(nameof(Tasks));
        }
    }
}
