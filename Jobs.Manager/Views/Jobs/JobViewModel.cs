using System.ComponentModel;

namespace Jobs.Manager.Views.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class JobViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
