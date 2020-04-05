using System.Collections.ObjectModel;
using Jobs.Manager.Views.Jobs.Models;
using Jobs.Manager.Views.Tasks.Models;
using Jobs.Tasks.Common.Models;

namespace Jobs.Manager.Views.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class TasksViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public JobInfo JobInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => JobInfo.Name;
            set => JobInfo.Name = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<TaskInfo> Tasks
        {
            get => JobInfo.Tasks;
            set => JobInfo.Tasks = value;
        }
    }
}
