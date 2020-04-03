using RestSharp;

namespace Jobs.Tasks.Common.Logics.DataEditor
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskDataWorker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        JsonObject GetDataJson();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataJson"></param>
        void SetDataJson(JsonObject dataJson);
    }
}