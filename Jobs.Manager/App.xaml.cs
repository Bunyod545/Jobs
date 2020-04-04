using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Jobs.Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// 
        /// </summary>
        public const string TasksPath = "Tasks";

        /// <summary>
        /// 
        /// </summary>
        public const string LibraryFileExtensions = ".dll";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
            base.OnStartup(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name).Name;
            var filePath = Path.Combine(TasksPath, name) + LibraryFileExtensions;

            return File.Exists(filePath) ? Assembly.LoadFrom(filePath) : null;
        }
    }
}
