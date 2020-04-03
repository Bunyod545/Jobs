using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Jobs.Common.Logics.Tasks.Finder;
using Jobs.Common.Logics.Tasks.Finder.Models;
using Jobs.Manager.Views.Tasks.Models;

namespace Jobs.Manager.Views.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TaskEditView
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskInfo TaskInfo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static List<TaskLibraryInfo> TaskLibraryInfos => TaskFinder.GetTasksLibraryInfos();

        /// <summary>
        /// 
        /// </summary>
        public TaskEditView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskInfo"></param>
        public void SetTaskInfo(TaskInfo taskInfo)
        {
            TaskInfo = taskInfo;
            DataContext = TaskInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskLibrary_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var library = TaskLibrariesComboBox.SelectedValue as TaskLibraryInfo;
            if (library == null)
                return;

            TaskInfo.TaskLibraryPath = library.LibraryPath;
            var tasks = TaskFinder.GetTasksList(library.LibraryPath);
            TaskClassesComboBox.Items.Clear();
            tasks.ForEach(f => TaskClassesComboBox.Items.Add(f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
