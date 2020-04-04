using Jobs.Tasks.Common.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Tasks;

namespace Test.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TestTask : ITask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskLogService _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public TestTask(ITaskLogService log)
        {
            _log = log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            _log.Success("Hello Task!");
            _log.Success($"My Name is: {Name}");
            return true;
        }
    }
}
