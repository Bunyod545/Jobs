using System.Windows;
using Newtonsoft.Json;
using RestSharp;

namespace Jobs.Tasks.Common.Logics.DataEditor
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseTaskDataEditor : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public ITaskDataWorker DataWorker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetTaskData<T>()
        {
            var jsonData = DataWorker.GetDataJson();
            if (jsonData == null)
                return default(T);

            var jsonDataString = JsonConvert.SerializeObject(jsonData);
            return JsonConvert.DeserializeObject<T>(jsonDataString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public virtual void SetTaskData(object data)
        {
            if (data == null)
            {
                DataWorker.SetDataJson(null);
                SuccessSave();
                return;
            }

            var dataJsonString = JsonConvert.SerializeObject(data);
            var dataJson = JsonConvert.DeserializeObject<JsonObject>(dataJsonString);
            DataWorker.SetDataJson(dataJson);
            SuccessSave();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void SuccessSave()
        {
            MessageBox.Show("Success save!");
        }
    }
}
