using System.Threading.Tasks;
using System.Windows;
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
            JobInfo = jobInfo;
            DataContext = jobInfo;
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
        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            var jobExecuter = new JobExecuter(JobInfo.Source);
            jobExecuter.Container.Register<ITaskLogService>(new TaskLogService(this));
            jobExecuter.Container.Register<ITaskCustomizedLogService>(new TaskCustomizedLogService(this));
            jobExecuter.Initialize();
            ExecuteButton.IsEnabled = false;

            ThreadTask.Run(() => jobExecuter.Execute()).ContinueWith(OnExecuteFinished);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnExecuteFinished(Task<bool> result)
        {
            Dispatcher.Invoke(() => { ExecuteButton.IsEnabled = true; });
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
    }
}
