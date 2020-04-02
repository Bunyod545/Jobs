using Jobs.Tasks.Common.Logics.Services.Log;

namespace SystemctlService.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemctlStopTask : SystemctlBaseTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public SystemctlStopTask(ITaskLogService log) : base(log)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetCommandName()
        {
            return "stop";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool IsWaitStatusChange()
        {
            return true;
        }
    }
}
