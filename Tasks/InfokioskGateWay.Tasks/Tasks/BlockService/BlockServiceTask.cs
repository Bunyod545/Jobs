using System.Net.Http;
using System.Threading;
using Jobs.Tasks.Common.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Tasks;

namespace InfokioskGateWay.Tasks.Tasks.BlockService
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BlockServiceTask : ITask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskLogService _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public BlockServiceTask(ITaskLogService log)
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
            var result = httpClient.GetAsync(BlockServiceUrl);
            result.Wait();

            if (!result.Result.IsSuccessStatusCode)
            {
                _log.Warning(result.Result?.Content?.ReadAsStringAsync().Result ?? "Empty content");
                _log.Error("Block service faulted");

                httpClient.Dispose();
                return false;
            }

            _log.Success(result.Result?.Content?.ReadAsStringAsync().Result ?? "Empty content");
            _log.Information("Waiting 3 seconds");
            httpClient.Dispose();

            Thread.Sleep(3000);
            _log.Information("Waiting 3 seconds finished");
            _log.Information("Block service finished");

            return true;
        }
    }
}
