using Jobs.Common.Database;
using Jobs.Common.Database.Tables;

namespace Jobs.Manager.Views.Jobs.JobsViews.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Task _source;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public TaskInfo(Task task)
        {
            _source = task;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            JobsDatabase.Tasks.Update(_source);
        }
    }
}
