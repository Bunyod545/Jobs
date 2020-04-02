using Jobs.Common.Database;
using Jobs.Common.Database.Tables;

namespace Jobs.Manager.Views.Jobs.JobsViews.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JobInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Job _source;

        /// <summary>
        /// 
        /// </summary>
        public int Id => _source.Id;

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => _source.Name;
            set => _source.Name = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get => _source.Description;
            set => _source.Description = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public JobInfo(Job source)
        {
            _source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            JobsDatabase.Jobs.Update(_source);
        }
    }
}
