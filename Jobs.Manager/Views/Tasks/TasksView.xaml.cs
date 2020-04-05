using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Jobs.Common.Database.Tables;
using Jobs.Common.Logics.Jobs;
using Jobs.Common.Logics.Tasks.DataEditor;
using Jobs.Manager.Logics.Services.Log;
using Jobs.Manager.Views.Jobs.Models;
using Jobs.Manager.Views.Tasks.Models;
using Jobs.Tasks.Common.Logics.Services.Log;
using Task = Jobs.Common.Database.Tables.Task;
using ThreadTask = System.Threading.Tasks.Task;

namespace Jobs.Manager.Views.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TasksView
    {
        /// <summary>
        /// 
        /// </summary>
        public JobInfo JobInfo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public TasksViewModel Model { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public TasksView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobInfo"></param>
        public void SetJobInfo(JobInfo jobInfo)
        {
            RichTextBox.Document.Blocks.Clear();
            JobInfo = jobInfo;

            Model = new TasksViewModel();
            Model.JobInfo = jobInfo;

            DataContext = Model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new TaskEditView();
            window.SetTaskInfo(new TaskInfo(JobInfo, new Task()));

            var result = window.ShowDialog();
            if (result != true)
                return;

            JobInfo.AddTask(window.TaskInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGoToJobs_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowJobsView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ListViewContextMenu.Tag = ((FrameworkElement)e.OriginalSource).DataContext as TaskInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var taskInfo = ListViewContextMenu.Tag as TaskInfo;
            if (taskInfo == null)
                return;

            var job = new Job();
            job.Name = JobInfo.Name;
            job.Description = JobInfo.Description;

            var task = taskInfo.Source.Clone();
            task.IsChecked = true;

            job.Tasks.Add(task);
            JobExecute(job);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            JobExecute(JobInfo.Source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        private void JobExecute(Job job)
        {
            var jobExecuter = new JobExecuter(job);
            jobExecuter.Container.Register<ITaskLogService>(new TaskLogService(this));
            jobExecuter.Container.Register<ITaskCustomizedLogService>(new TaskCustomizedLogService(this));
            jobExecuter.Initialize();

            DisableComponents();
            ThreadTask.Run(() => jobExecuter.Execute()).ContinueWith(OnExecuteFinished);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnExecuteFinished(Task<bool> result)
        {
            Dispatcher.Invoke(EnableComponents);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskItemComponent_OnData(object sender, TaskInfo e)
        {
            var dataEditor = TaskDataEditorManager.GetDataEditor(e.Source);
            if (dataEditor == null)
                return;

            dataEditor.DataWorker = new TaskDataWorker(e.Source);
            dataEditor.ShowDialog();
            JobInfo.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskItemComponent_OnEdit(object sender, TaskInfo e)
        {
            var window = new TaskEditView();
            window.SetTaskInfo(new TaskInfo(e.Source.Clone()));

            var submitResult = window.ShowDialog();
            if (submitResult != true)
                return;

            e.Name = window.TaskInfo.Name;
            e.TaskLibraryPath = window.TaskInfo.TaskLibraryPath;
            e.TaskClassName = window.TaskInfo.TaskClassName;
            JobInfo.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskItemComponent_OnDelete(object sender, TaskInfo e)
        {
            JobInfo.DeleteTask(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox.Document.Blocks.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisableComponents()
        {
            SetComponentsEnable(false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void EnableComponents()
        {
            SetComponentsEnable(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void SetComponentsEnable(bool value)
        {
            TasksListBox.IsEnabled = value;
            AddTaskButton.IsEnabled = value;
            ClearButton.IsEnabled = value;
            GoToJobsButton.IsEnabled = value;
            ExecuteButton.IsEnabled = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PreviewMouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (!(sender is ListBoxItem) || e.LeftButton != MouseButtonState.Pressed)
                return;

            var draggedItem = (ListBoxItem)sender;
            DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
            draggedItem.IsSelected = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            if (!(sender is ListBoxItem))
                return;

            var droppedData = e.Data.GetData(typeof(TaskInfo)) as TaskInfo;
            var target = ((ListBoxItem)sender).DataContext as TaskInfo;

            if (droppedData == null || target == null)
                return;

            var removedIdx = TasksListBox.Items.IndexOf(droppedData);
            var targetIdx = TasksListBox.Items.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                DropTask(targetIdx + 1, droppedData, removedIdx);
                return;
            }

            var remIdx = removedIdx + 1;
            if (JobInfo.Tasks.Count + 1 > remIdx)
                DropTask(targetIdx, droppedData, removedIdx + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DropTask(int targetIndex, TaskInfo droppedData, int removedIndex)
        {
            JobInfo.Source.Tasks.Insert(targetIndex, droppedData.Source);
            JobInfo.Source.Tasks.RemoveAt(removedIndex);

            JobInfo.Tasks.Insert(targetIndex, droppedData);
            JobInfo.Tasks.RemoveAt(removedIndex);
            JobInfo.Update();
        }
    }
}
