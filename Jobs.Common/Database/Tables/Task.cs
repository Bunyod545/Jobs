using Newtonsoft.Json;
using RestSharp;

namespace Jobs.Common.Database.Tables
{
    /// <summary>
    /// 
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RegisteredTask RegisteredTask { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskData"></param>
        public void SetTaskData(JsonObject taskData)
        {
            TaskData = JsonConvert.SerializeObject(taskData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonObject GetTaskData()
        {
            if (string.IsNullOrEmpty(TaskData))
                return null;

            return JsonConvert.DeserializeObject<JsonObject>(TaskData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
