using System.Collections.ObjectModel;
using Jobs.Common.Database.Tables;
using Jobs.Manager.Views.Tasks;

namespace Jobs.Manager.Views.Jobs
{
    /// <summary>
    ///
    /// </summary>
    public partial class JobsView
    {
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
            ViewModel = new JobsViewModel();
            ViewModel.JobGroups = new ObservableCollection<JobGroup>();
            var jobGroup1 = new JobGroup();
            jobGroup1.Name = "Parent1";

            var jobGroup1Child1 = new JobGroup();
            jobGroup1Child1.Name = "Child1";
            jobGroup1.ChildGroups.Add(jobGroup1Child1);

            var jobGroup1Child2 = new JobGroup();
            jobGroup1Child2.Name = "Child2";
            jobGroup1.ChildGroups.Add(jobGroup1Child2);

            ViewModel.JobGroups.Add(jobGroup1);

            var jobGroup2 = new JobGroup();
            jobGroup2.Name = "Parent2";
            ViewModel.JobGroups.Add(jobGroup2);

            var job = new Job();
            job.NameText = "Merchant update";
            job.DescriptionText = "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test";

            ViewModel.Jobs.Add(job);
            ViewModel.Jobs.Add(job);
            ViewModel.Jobs.Add(job);
            ViewModel.Jobs.Add(job);
            ViewModel.Jobs.Add(job);
            ViewModel.Jobs.Add(job);
            ViewModel.Jobs.Add(job);

            DataContext = ViewModel;
        }
    }
}
