using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Jobs.Common.Database;
using Jobs.Common.Database.Tables;
using Jobs.Manager.Views.Jobs.JobsViews.Models;
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
            JobGroupsTree.SelectedItemChanged += JobGroupsTreeOnSelectedItemChanged;

            ViewModel = new JobsViewModel();
            ViewModel.JobGroups = new ObservableCollection<JobGroupInfo>();

            DataContext = ViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobGroupsTreeOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var group = e.NewValue as JobGroupInfo;
            if (group == null)
                return;

            ViewModel.SelectedJobGroup = group;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveMenuItem_OnClick(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var jobGroup = new JobGroup();
            jobGroup.Name = JobAddTextBox.Text;
            JobsDatabase.JobGroups.Insert(jobGroup);

            if (JobGroupsTree.SelectedItem is JobGroup ownerGroup)
            {
                ownerGroup.ChildGroups.Add(jobGroup);
                JobsDatabase.JobGroups.Update(ownerGroup);
            }
        }
    }
}
