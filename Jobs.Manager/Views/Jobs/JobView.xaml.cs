using System;
using System.Windows;
using Jobs.Manager.Views.Jobs.JobsViews.Models;

namespace Jobs.Manager.Views.Jobs
{
    /// <summary>
    ///
    /// </summary>
    public partial class JobView
    {
        /// <summary>
        /// 
        /// </summary>
        public JobInfo Model => DataContext as JobInfo;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<JobInfo> Delete;

        /// <summary>
        /// 
        /// </summary>
        public JobView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Model.IsEdit)
                Model.Update();

            Model.IsEdit = !Model.IsEdit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(this, Model);
        }
    }
}
