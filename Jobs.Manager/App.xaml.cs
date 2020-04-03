using System;
using System.Windows;
using Jobs.Common.Logics.Tasks.Finder;

namespace Jobs.Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = TaskFinder.TasksFolder;
            AppDomain.CurrentDomain.SetupInformation.PrivateBinPathProbe = TaskFinder.TasksFolder;
            base.OnStartup(e);
        }
    }
}
