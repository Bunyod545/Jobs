using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Jobs.Common.Logics.TaskRegistrator
{
    /// <summary>
    /// 
    /// </summary>
    public static class TaskRegistratorManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<string> TaskFolders { get; }

        /// <summary>
        /// 
        /// </summary>
        static TaskRegistratorManager()
        {
            TaskFolders = new List<string>();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskPath"></param>
        public static void RegisterAssembly(string taskPath)
        {
            if (TaskFolders.Contains(taskPath))
                return;

            TaskFolders.Add(taskPath);
            var file = File.ReadAllBytes(taskPath);

            Assembly.Load(file);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = args.RequestingAssembly?.GetName().Name;
            if (assemblyName == null)
                return null;
            
            var menuPath = Path.Combine("Tasks", assemblyName);
            if (!Directory.Exists(menuPath))
                return null;

            var name = new AssemblyName(args.Name).Name;
            var filePath = Path.Combine(menuPath, name) + ".dll";

            if (!File.Exists(filePath))
                return null;

            return Assembly.LoadFrom(filePath);
        }
    }
}
