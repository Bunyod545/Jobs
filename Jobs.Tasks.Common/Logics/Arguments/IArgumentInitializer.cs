using Jobs.Tasks.Common.Logics.Tasks;
using RestSharp;

namespace Jobs.Tasks.Common.Logics.Arguments
{
    /// <summary>
    /// 
    /// </summary>
    public interface IArgumentInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="taskData"></param>
        void SetArguments(ITask task, JsonObject taskData);
    }
}