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
        public static ILiteCollection<Job> Jobs { get; set; }

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
            Jobs = Database.GetCollection<Job>(nameof(Jobs));
        }
    }
}
