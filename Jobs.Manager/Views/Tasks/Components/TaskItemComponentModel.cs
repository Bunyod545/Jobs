using System.ComponentModel;

namespace Jobs.Manager.Views.Tasks.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskItemComponentModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}