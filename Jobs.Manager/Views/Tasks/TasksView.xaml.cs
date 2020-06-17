using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Jobs.Common.Database.Tables;
using Jobs.Common.Logics.Jobs;
using Jobs.Common.Logics.Tasks.DataEditor;
using Jobs.Manager.Helpers;
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
        private void ExecuteMenuItem_OnClick(object sender, RoutedEventArgs e)
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
        private void MoveUpMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var taskInfo = ListViewContextMenu.Tag as TaskInfo;
            if (taskInfo == null)
                return;

            var index = JobInfo.Tasks.IndexOf(taskInfo);
            if (index <= 0)
                return;

            MoveTask(index - 1, taskInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveDownMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var taskInfo = ListViewContextMenu.Tag as TaskInfo;
            if (taskInfo == null)
                return;

            var index = JobInfo.Tasks.IndexOf(taskInfo);
            if (index == -1)
                return;

            var count = JobInfo.Tasks.Count;
            if (count == index + 1)
                return;

            MoveTask(index + 1, taskInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="taskInfo"></param>
        private void MoveTask(int index, TaskInfo taskInfo)
        {
            JobInfo.Source.Tasks.Remove(taskInfo.Source);
            JobInfo.Source.Tasks.Insert(index, taskInfo.Source);

            JobInfo.Tasks.Remove(taskInfo);
            JobInfo.Tasks.Insert(index, taskInfo);
            JobInfo.Update();
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
            var logService = new TaskLogService(this);

            var jobExecuter = new JobExecuter(job);
            jobExecuter.Container.Register<ITaskLogService>(logService);
            jobExecuter.Container.Register<ITaskCustomizedLogService>(new TaskCustomizedLogService(this));
            jobExecuter.Initialize();
            jobExecuter.TaskExecuted += executer => logService.Information(Environment.NewLine);

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
            if (!DeleteHelper.IsAreYouSure())
                return;

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
    }
}
