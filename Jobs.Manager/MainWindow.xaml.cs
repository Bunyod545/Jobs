using System.Linq;
using System.Windows;
using Jobs.Common.Database;
using Jobs.Common.Database.Tables;
using Jobs.Common.Logics.Jobs;
using RestSharp;

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
            var db = new JobsDatabase();

            var group = new JobGroup();
            group.Name = "Test group";

            var job = new Job();
            job.Name = "AdminService update";
            job.Description = "Admin service publish job";
         
            var task = new Task();
            task.Name = "DotnetPublish";
            task.TaskClassName = "PublishTask";
            task.TaskLibraryPath = "Tasks\\DotnetPublish.Tasks\\DotnetPublish.Tasks.dll";

            task.TaskData = new JsonObject();
            task.TaskData.Add("ProjectPath", "D:\\Projects\\PaymentSystemsGitLab\\src\\Common\\Services\\Admin\\AdminService\\AdminService.csproj");
            task.TaskData.Add("Configuration", "Release");
            task.TaskData.Add("Framework", "netcoreapp2.2");
            task.TaskData.Add("Runtime", "linux-x64");


            job.Tasks.Add(task);
            group.Jobs.Add(job);
            db.JobGroups.Insert(group);
        }
    }
}
