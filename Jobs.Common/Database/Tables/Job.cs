using System.Collections.Generic;

namespace Jobs.Common.Database.Tables
{
    /// <summary>
    /// 
    /// </summary>
    public class Job
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Task> Tasks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Job()
        {
            Tasks = new List<Task>();
        }
    }
}
