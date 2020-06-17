using System.Net.Http;
using System.Threading;
using Jobs.Tasks.Common.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Tasks;

namespace InfokioskGateWay.Tasks.Tasks.UnlockService
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UnlockServiceTask : ITask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskLogService _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public UnlockServiceTask(ITaskLogService log)
        {
            _log = log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            var httpClient = new HttpClient();
            var result = httpClient.GetAsync(UnlockServiceUrl);
            result.Wait();

            if (!result.Result.IsSuccessStatusCode)
            {
                _log.Warning(result.Result?.Content?.ReadAsStringAsync().Result ?? "Empty content");
                _log.Error("Unlock service faulted");

                httpClient.Dispose();
                return false;
            }

            _log.Success(result.Result?.Content?.ReadAsStringAsync().Result ?? "Empty content");
            _log.Information("Unlock service finished");
         
            httpClient.Dispose();
            return true;
        }
    }
}
