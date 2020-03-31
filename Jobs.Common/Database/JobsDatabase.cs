using Jobs.Common.Database.Tables;
using LiteDB;

namespace Jobs.Common.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class JobsDatabase
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DatabaseName = "Jobs.db";

        /// <summary>
        /// 
        /// </summary>
        public ILiteCollection<JobGroup> Jobs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LiteDatabase Database { get; }

        /// <summary>
        /// 
        /// </summary>
        public JobsDatabase()
        {
            Database = new LiteDatabase(DatabaseName);
            Database.GetCollection<JobGroup>();
        }
    }
}
