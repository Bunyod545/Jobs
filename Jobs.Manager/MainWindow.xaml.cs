using System.Collections.Generic;
using System.Windows;
using SystemctlService.Tasks;
using Jobs.Common.Database;
using Jobs.Common.Database.Tables;
using Jobs.Manager.Logics.Services.Log;
using RestSharp;
using Sftp.Tasks;

namespace Jobs.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var task = new SystemctlStatusTask(null);
            task.SftpHost = "172.17.8.131";
            task.SftpLogin = "uzcard_admin";
            task.SftpPassword = "cU[a+A!#$~Wu6/?(";
            task.ServiceName = "payment_system_infokiosk_task_service.service";

            task.Execute();
            return;


            var group = new JobGroup();
            group.Name = "Test group";

            var job = new Job();
            job.Name = "AdminService update";
            job.Description = "Admin service publish job";

            group.Jobs = new List<Job>();
            group.Jobs.Add(job);

            ////var task = new Task();
            ////task.Name = "DotnetPublish";
            ////task.TaskClassName = "PublishTask";
            ////task.TaskLibraryPath = "Tasks\\DotnetPublish.Tasks\\DotnetPublish.Tasks.dll";

            ////var taskData = new JsonObject();
            ////taskData.Add("ProjectPath", @"D:\Projects\PaymentSystemsGitLab\src\Common\Services\Admin\AdminService\AdminService.csproj");
            ////taskData.Add("Configuration", "Release");
            ////taskData.Add("Framework", "netcoreapp2.2");
            ////taskData.Add("Runtime", "linux-x64");
            //task.SetTaskData(taskData);

            //JobsDatabase.Tasks.Insert(task);
            //JobsDatabase.Jobs.Insert(job);
            JobsDatabase.JobGroups.Insert(group);
        }
    }
}
