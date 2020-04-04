using System.Linq;
using System.Reflection;
using Jobs.Tasks.Common.Logics.Arguments;
using Jobs.Tasks.Common.Logics.Tasks;
using RestSharp;

namespace Jobs.Common.Logics.Tasks.Logics.Arguments
{
    /// <summary>
    /// 
    /// </summary>
    public class ArgumentInitializer : IArgumentInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="taskData"></param>
        public void SetArguments(ITask task, JsonObject taskData)
        {
            var props = task.GetType().GetProperties().ToList();
            props = props.Where(w => w.CanWrite).ToList();
            props.ForEach(f => SetPropertyValue(f, task, taskData));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="task"></param>
        /// <param name="taskData"></param>
        private void SetPropertyValue(PropertyInfo prop, ITask task, JsonObject taskData)
        {
            if (taskData.TryGetValue(prop.Name, out var value) && value != null)
                prop.SetValue(task, value);
        }
    }
}