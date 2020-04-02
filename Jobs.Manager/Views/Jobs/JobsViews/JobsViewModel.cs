using System.Collections.ObjectModel;
using System.ComponentModel;
using Jobs.Common.Database.Tables;
using Jobs.Manager.Views.Jobs.JobsViews.Models;

namespace Jobs.Manager.Views.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class JobsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<JobGroupInfo> JobGroups { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobGroupInfo SelectedJobGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<JobInfo> Jobs { get; set; } = new ObservableCollection<JobInfo>();

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
