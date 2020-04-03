using Jobs.Common.Database;
using Jobs.Common.Database.Tables;
using Jobs.Tasks.Common.Logics.DataEditor;
using RestSharp;

namespace Jobs.Common.Logics.Tasks.DataEditor
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskDataWorker : ITaskDataWorker
    {
        /// <summary>
        /// 
        /// </summary>
        public Task Task { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public TaskDataWorker(Task task)
        {
            Task = task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonObject GetDataJson()
        {
            return Task.GetTaskData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataJson"></param>
        public void SetDataJson(JsonObject dataJson)
        {
            Task.SetTaskData(dataJson);
        }
    }
}