using Jobs.Tasks.Common.Logics.Services.Log;

namespace SystemctlService.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemctlRestartTask : SystemctlBaseTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public SystemctlRestartTask(ITaskLogService log) : base(log)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Execute()
        {
            return true;
        }
    }
}
