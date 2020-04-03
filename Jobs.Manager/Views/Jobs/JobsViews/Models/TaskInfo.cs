using Jobs.Common.Database.Tables;
using Jobs.Tasks.Common.Models;

namespace Jobs.Manager.Views.Jobs.JobsViews.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInfo : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly Task Source;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public TaskInfo(Task task)
        {
            Source = task;
        }
    }
}
