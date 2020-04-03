using System;
using System.Linq;
using System.Reflection;
using Jobs.Common.Logics.TaskRegistrator;
using Task = Jobs.Common.Database.Tables.Task;

namespace Jobs.Common.Logics.Tasks.Finder
{
    /// <summary>
    /// 
    /// </summary>
    public static class TaskFinder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static Type FindType(Task task)
        {
            TaskRegistratorManager.RegisterAssembly(task.TaskLibraryPath);

            var assembly = Assembly.LoadFrom(task.TaskLibraryPath);
            return assembly.GetTypes().FirstOrDefault(f => f.Name == task.TaskClassName);
        }
    }
}
