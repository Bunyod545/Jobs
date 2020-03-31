using System;
using System.Reflection;
using Jobs.Common.Logics.Tasks.Logics.Container;
using Jobs.Tasks.Common.Logics.Initializer;
using Jobs.Tasks.Common.Logics.Initializer.Attributes;

namespace Jobs.Common.Logics.Tasks.Logics.Initializer
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskType"></param>
        public void Initialize(Type taskType)
        {
            var initAttr = taskType.GetCustomAttribute<TaskInitializerAttribute>();
            if (initAttr == null)
                return;

            var initializer = Activator.CreateInstance(initAttr.InitalizerType) as ITaskInitializer;
            if (initializer == null)
                return;

            var options = new TaskInitializeOptions();
            options.Container = new TaskServicesRegistrator();

            initializer.Initialize(options);
        }
    }
}
