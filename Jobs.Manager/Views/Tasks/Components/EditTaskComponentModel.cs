using System.ComponentModel;
using System.Runtime.CompilerServices;
using Jobs.Manager.Annotations;

namespace Jobs.Manager.Views.Tasks.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class EditTaskComponentModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}