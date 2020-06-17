using SystemctlService.Tasks.Views;
using Jobs.Tasks.Common.Logics.DataEditor.Attributes;
using Jobs.Tasks.Common.Logics.Services.Log;

namespace SystemctlService.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(SystemctlStatusTaskEditor))]
    public class SystemctlStatusTask : SystemctlBaseTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public SystemctlStatusTask(ITaskLogService log) : base(log)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetCommandName()
        {
            return "status";
        }
    }
}
