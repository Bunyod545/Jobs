using Jobs.Common.Database;
using Jobs.Common.Database.Tables;
using Jobs.Tasks.Common.Models;

namespace Jobs.Manager.Views.Jobs.JobsViews.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JobInfo : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly Job Source;

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => Source.Name;
            set => Source.Name = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get => Source.Description;
            set => Source.Description = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEdit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public JobInfo(Job source)
        {
            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            JobsDatabase.Jobs.Update(Source);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {
            JobsDatabase.Jobs.Delete(Source.Id);
        }
    }
}
