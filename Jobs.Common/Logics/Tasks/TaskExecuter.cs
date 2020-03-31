using System;
using Jobs.Common.Database.Tables;
using Jobs.Common.Helpers;
using Jobs.Common.Logics.Container;
using Jobs.Common.Logics.Tasks.Logics.Arguments;
using Jobs.Common.Logics.Tasks.Logics.Container;
using Jobs.Common.Logics.Tasks.Logics.Finder;
using Jobs.Common.Logics.Tasks.Logics.Initializer;
using Jobs.Tasks.Common;
using Jobs.Tasks.Common.Logics.Arguments;
using Jobs.Tasks.Common.Logics.Services.Container;

namespace Jobs.Common.Logics.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskExecuter
    {
        /// <summary>
        /// 
        /// </summary>
        private Type _taskType;

        /// <summary>
        /// 
        /// </summary>
        public Task Task { get; }

        /// <summary>
        /// 
        /// </summary>
        public TaskFinder TaskFinder { get; }

        /// <summary>
        /// 
        /// </summary>
        public TaskInitializer TaskInitializer { get; }

        /// <summary>
        /// 
        /// </summary>
        public TaskArgumentInitializer TaskArgumentInitializer { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public TaskExecuter(Task task)
        {
            Task = task;
            TaskFinder = new TaskFinder(task);
            TaskInitializer = new TaskInitializer();
            TaskArgumentInitializer = new TaskArgumentInitializer();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            _taskType = TaskFinder.FindType(Task);
            if (_taskType == null)
                return;

            ServicesContainer.Current.Register<ITaskServicesContainer, TaskServicesContainer>();
            ServicesContainer.Current.Register<IArgumentInitializer, ArgumentInitializer>();
            TaskInitializer.Initialize(_taskType);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Execute()
        {
            if (_taskType == null)
                return false;

            var task = (ITask)ActivatorHelper.CreateInstance(_taskType);
            if (task == null)
                return false;

            TaskArgumentInitializer.SetArguments(task, Task.TaskData);
            return task.Execute();
        }
    }
}
