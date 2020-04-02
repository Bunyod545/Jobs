using Jobs.Tasks.Common.Logics.Services.Log;

namespace SystemctlService.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemctlStartTask : SystemctlBaseTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public SystemctlStartTask(ITaskLogService log) : base(log)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetCommandName()
        {
            return "start";
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
