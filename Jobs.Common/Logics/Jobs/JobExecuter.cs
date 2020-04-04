using Jobs.Common.Database.Tables;
using Jobs.Common.Logics.Tasks;
using System.Linq;
using Jobs.Common.Logics.Container;
using Jobs.Common.Logics.Tasks.Executer;

namespace Jobs.Common.Logics.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class JobExecuter
    {
        /// <summary>
        /// 
        /// </summary>
        public static JobExecuter Current { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Job Job { get; }

        /// <summary>
        /// 
        /// </summary>
        public ServicesContainer Container { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        public JobExecuter(Job job)
        {
            Job = job;
            Container = new ServicesContainer();
            Current = this;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Execute()
        {
            if (Job?.Tasks == null)
                return true;

            var taskExecuters = Job.Tasks
                .Where(w => w.IsChecked)
                .Select(s => new TaskExecuter(s)).ToList();

            taskExecuters.ForEach(f => f.Initialize());
            Container.Initialize();

            return taskExecuters.All(a => a.Execute());
        }
    }
}
