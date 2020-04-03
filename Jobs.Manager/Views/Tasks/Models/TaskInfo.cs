using Jobs.Common.Database.Tables;
using Jobs.Manager.Views.Jobs.Models;
using Jobs.Tasks.Common.Models;

namespace Jobs.Manager.Views.Tasks.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInfo : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly JobInfo JobInfo;

        /// <summary>
        /// 
        /// </summary>
        public readonly Task Source;

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => Source.Name;
            set => Source.Name = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TaskLibraryPath
        {
            get => Source.TaskLibraryPath;
            set => Source.TaskLibraryPath = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TaskClassName
        {
            get => Source.TaskClassName;
            set => Source.TaskClassName = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked
        {
            get => Source.IsChecked;
            set
            {
                Source.IsChecked = value;
                JobInfo?.Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Order
        {
            get => Source.Order;
            set => Source.Order = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public TaskInfo(Task task) : this(null, task)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <param name="source"></param>
        public TaskInfo(JobInfo jobInfo, Task source)
        {
            JobInfo = jobInfo;
            Source = source;
        }
    }
}
