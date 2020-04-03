using System;
using System.Windows;
using Jobs.Manager.Views.Tasks.Models;

namespace Jobs.Manager.Views.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TaskItemComponent
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskInfo TaskInfo => DataContext as TaskInfo;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TaskInfo> TaskEdit;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TaskInfo> TaskDelete;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TaskInfo> TaskData;

        /// <summary>
        /// 
        /// </summary>
        public TaskItemComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataButton_Click(object sender, RoutedEventArgs e)
        {
            TaskData?.Invoke(this, TaskInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TaskEdit?.Invoke(this, TaskInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TaskDelete?.Invoke(this, TaskInfo);
        }
    }
}
