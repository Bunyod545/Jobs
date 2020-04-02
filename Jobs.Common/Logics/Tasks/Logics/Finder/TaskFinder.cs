using System;
using System.Linq;
using System.Reflection;
using Jobs.Common.Database.Tables;
using Jobs.Common.Logics.TaskRegistrator;

namespace Jobs.Common.Logics.Tasks.Logics.Finder
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskFinder
    {
        /// <summary>
        /// 
        /// </summary>
        public Task Task { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public TaskFinder(Task task)
        {
            Task = task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Type FindType(Task task)
        {
            var taskInfo = task.RegisteredTask;
            TaskRegistratorManager.RegisterAssembly(taskInfo.TaskLibraryPath);

            var assembly = Assembly.LoadFrom(taskInfo.TaskLibraryPath);
            return assembly.GetTypes().FirstOrDefault(f => f.Name == taskInfo.TaskClassName);
        }
    }
}
