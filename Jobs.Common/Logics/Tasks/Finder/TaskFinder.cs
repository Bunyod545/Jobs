using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Jobs.Common.Logics.TaskRegistrator;
using Jobs.Common.Logics.Tasks.Finder.Models;
using Jobs.Tasks.Common;
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
        public const string TasksFolder = "Tasks";

        /// <summary>
        /// 
        /// </summary>
        public const string TaskSearchPattern = "*.Tasks.dll";

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<TaskLibraryInfo> GetTasksLibraryInfos()
        {
            var tasksFolder = new DirectoryInfo(TasksFolder);
            var files = tasksFolder.GetFiles(TaskSearchPattern);

            return files.Select(s => new TaskLibraryInfo(s)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetTasksList(string path)
        {
            var assembly = Assembly.LoadFrom(path);

            var taskType = typeof(ITask);
            var tasks = assembly.GetTypes().Where(w => 
                    w.IsClass && 
                    !w.IsAbstract && 
                    taskType.IsAssignableFrom(w)).ToList();

            return tasks.Select(s => s.Name).ToList();
        }
    }
}
