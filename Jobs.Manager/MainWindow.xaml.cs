using Jobs.Manager.Views.Jobs.JobsViews;
using Jobs.Manager.Views.Jobs.Models;
using Jobs.Manager.Views.Tasks;

namespace Jobs.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// 
        /// </summary>
        public static MainWindow Current { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static JobsView JobsView { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static TasksView TasksView { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Current = this;
            JobsView = new JobsView();
            TasksView = new TasksView();

            ContentGrid.Children.Add(JobsView);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ShowJobsView()
        {
            Current.ContentGrid.Children.Clear();
            Current.ContentGrid.Children.Add(JobsView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobInfo"></param>
        public static void ShowTasksView(JobInfo jobInfo)
        {
            TasksView.SetJobInfo(jobInfo);
            Current.ContentGrid.Children.Clear();
            Current.ContentGrid.Children.Add(TasksView);
        }
    }
}