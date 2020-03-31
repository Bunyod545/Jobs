using System;
using System.Linq;
using System.Reflection;
using Jobs.Common.Database.Tables;

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
            var assembly = Assembly.LoadFrom(task.TaskLibraryPath);
            return assembly.GetTypes().FirstOrDefault(f => f.Name == task.TaskClassName);
        }
    }
}
