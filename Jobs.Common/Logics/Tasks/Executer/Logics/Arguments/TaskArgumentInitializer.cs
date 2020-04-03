using System.Reflection;
using Jobs.Common.Helpers;
using Jobs.Tasks.Common;
using Jobs.Tasks.Common.Logics.Arguments;
using Jobs.Tasks.Common.Logics.Arguments.Attributes;
using RestSharp;

namespace Jobs.Common.Logics.Tasks.Logics.Arguments
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskArgumentInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="taskData"></param>
        public void SetArguments(ITask task, JsonObject taskData)
        {
            if (task == null || taskData == null)
                return;

            var defaultArgInitializer = new ArgumentInitializer();
            var attr = task.GetType().GetCustomAttribute<ArgumentInitializerAttribute>();
            if (attr == null)
            {
                defaultArgInitializer.SetArguments(task, taskData);
                return;
            }

            var customArgInitializer = ActivatorHelper.CreateInstance(attr.ArgumentInitializerType) as IArgumentInitializer;
            var argInitializer = customArgInitializer ?? defaultArgInitializer;
            argInitializer?.SetArguments(task, taskData);
        }
    }
}