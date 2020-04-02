using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Jobs.Common.Database.Tables;
using Jobs.Common.Logics.Jobs;
using Jobs.Manager.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Services.Log;
using RestSharp;
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
        public Job Job { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TasksView()
        {
            InitializeComponent();

            Job = new Job();
            Job.Name = "DotnetPublish";
            //RegisterTestPublishTask();
            //RegisterTestSftpCopyTask();
            RegisterTestSystemctlRestartTask();
            //RegisterTestSystemctlStatusTask();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void RegisterTestPublishTask()
        {
            var task = new Task();
            task.Name = "DotnetPublish";
            task.RegisteredTask = new RegisteredTask();
            task.RegisteredTask.TaskClassName = "PublishTask";
            task.RegisteredTask.TaskLibraryPath = "Tasks\\DotnetPublish.Tasks\\DotnetPublish.Tasks.dll";

            var taskData = new JsonObject();
            taskData.Add("ProjectPath", "D:\\Projects\\PaymentSystemsGitLab\\src\\Common\\Services\\Admin\\AdminService\\AdminService.csproj");
            taskData.Add("Configuration", "Release");
            taskData.Add("Framework", "netcoreapp2.2");
            taskData.Add("Runtime", "linux-x64");
            task.SetTaskData(taskData);

            TasksListBox.Items.Add(task);
            Job.Tasks.Add(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void RegisterTestSftpCopyTask()
        {
            var task = new Task();
            task.Name = "SftpCopyTask";
            task.RegisteredTask = new RegisteredTask();
            task.RegisteredTask.TaskClassName = "SftpCopyTask";
            task.RegisteredTask.TaskLibraryPath = "Tasks\\Sftp.Tasks\\Sftp.Tasks.dll";

            var taskData = new JsonObject();
            taskData.Add("SftpHost", "172.17.8.131");
            taskData.Add("SftpLogin", "uzcard_admin");
            taskData.Add("SftpPassword", "cU[a+A!#$~Wu6/?(");
            taskData.Add("FromPath", @"D:\Projects\PaymentSystemsGitLab\src\Common\Services\Admin\AdminService\bin\Release\netcoreapp2.2\linux-x64\publish\");
            taskData.Add("ToPath", "/home/uzcard_admin/Admin/Production");
            task.SetTaskData(taskData);

            TasksListBox.Items.Add(task);
            Job.Tasks.Add(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void RegisterTestSystemctlRestartTask()
        {
            var task = new Task();
            task.Name = "SystemctlRestartTask";
            task.RegisteredTask = new RegisteredTask();
            task.RegisteredTask.TaskClassName = "SystemctlRestartTask";
            task.RegisteredTask.TaskLibraryPath = "Tasks\\SystemctlService.Tasks\\SystemctlService.Tasks.dll";

            var taskData = new JsonObject();
            taskData.Add("SshHost", "172.17.8.131");
            taskData.Add("SshLogin", "uzcard_admin");
            taskData.Add("SshPassword", "cU[a+A!#$~Wu6/?(");
            taskData.Add("ServiceName", "payment_system_admin_service.service");
            task.SetTaskData(taskData);

            TasksListBox.Items.Add(task);
            Job.Tasks.Add(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void RegisterTestSystemctlStatusTask()
        {
            var task = new Task();
            task.Name = "SystemctlStatusTask";
            task.RegisteredTask = new RegisteredTask();
            task.RegisteredTask.TaskClassName = "SystemctlStatusTask";
            task.RegisteredTask.TaskLibraryPath = "Tasks\\SystemctlService.Tasks\\SystemctlService.Tasks.dll";

            var taskData = new JsonObject();
            taskData.Add("SshHost", "172.17.8.131");
            taskData.Add("SshLogin", "uzcard_admin");
            taskData.Add("SshPassword", "cU[a+A!#$~Wu6/?(");
            taskData.Add("ServiceName", "payment_system_admin_service.service");
            task.SetTaskData(taskData);

            //TasksListBox.Items.Add(task);
            //Job.Tasks.Add(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            var jobExecuter = new JobExecuter(Job);
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
    }
}
