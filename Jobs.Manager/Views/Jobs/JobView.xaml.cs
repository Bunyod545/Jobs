using System.ComponentModel;
using System.Windows;

namespace Jobs.Manager.Views.Jobs
{
    /// <summary>
    ///
    /// </summary>
    public partial class JobView : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty TitleTextProperty;

        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty DescriptonTextProperty;

        /// <summary>
        /// 
        /// </summary>
        static JobView()
        {
            TitleTextProperty = DependencyProperty.Register(nameof(TitleText), typeof(string), typeof(JobView));
            DescriptonTextProperty = DependencyProperty.Register(nameof(DescriptonText), typeof(string), typeof(JobView));
        }

        /// <summary>
        /// 
        /// </summary>
        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set => SetValue(TitleTextProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string DescriptonText
        {
            get => (string)GetValue(DescriptonTextProperty);
            set => SetValue(DescriptonTextProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public JobView()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
