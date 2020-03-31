using System.Collections.ObjectModel;
using System.ComponentModel;
using Jobs.Common.Database.Tables;

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
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<JobGroup> JobGroups { get; set; }
    }
}
