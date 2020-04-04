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
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskLibraryPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task Clone()
        {
            var task = new Task();
            task.Name = Name;
            task.IsChecked = IsChecked;
            task.Order = Order;
            task.TaskClassName = TaskClassName;
            task.TaskLibraryPath = TaskLibraryPath;
            task.TaskData = TaskData;

            return task;
        }
    }
}
