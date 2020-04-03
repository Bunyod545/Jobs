using System.Collections.ObjectModel;
using Jobs.Manager.Views.Jobs.JobsViews.Models;
using Jobs.Tasks.Common.Models;

namespace Jobs.Manager.Views.Jobs.JobsViews
{
    /// <summary>
    /// 
    /// </summary>
    public class JobsViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<JobInfo> Jobs { get; set; } = new ObservableCollection<JobInfo>();
    }
}
