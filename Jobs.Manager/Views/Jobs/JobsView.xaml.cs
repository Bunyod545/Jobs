using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Jobs.Common.Database;
using Jobs.Common.Database.Tables;
using Jobs.Manager.Helpers;
using Jobs.Manager.Views.Jobs.Models;
using Jobs.Manager.Views.Tasks;

namespace Jobs.Manager.Views.Jobs.JobsViews
{
    /// <summary>
    ///
    /// </summary>
    public partial class JobsView
    {
        /// <summary>
        /// 
        /// </summary>
        public static JobsView Current { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public JobsViewModel ViewModel { get; }

        /// <summary>
        /// 
        /// </summary>
        public JobsView()
        {
            InitializeComponent();
            Current = this;

            var jobs = JobsDatabase.Jobs.FindAll().ToList();
            var jobsInfos = jobs.Select(s => new JobInfo(s)).ToList();

            ViewModel = new JobsViewModel();
            ViewModel.Jobs = new ObservableCollection<JobInfo>(jobsInfos);

            DataContext = ViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobView_OnDelete(object sender, JobInfo e)
        {
            if (!DeleteHelper.IsAreYouSure())
                return;

            e.Delete();
            ViewModel.Jobs.Remove(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobView_OnImageClick(object sender, JobInfo e)
        {
            MainWindow.ShowTasksView(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddJob_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var job = new Job();
            JobsDatabase.Jobs.Insert(job);

            ViewModel.Jobs.Add(new JobInfo(job));
        }
    }
}
