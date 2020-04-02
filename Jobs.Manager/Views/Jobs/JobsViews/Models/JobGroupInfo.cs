using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Jobs.Common.Database;
using Jobs.Common.Database.Tables;

namespace Jobs.Manager.Views.Jobs.JobsViews.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JobGroupInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly JobGroup _source;

        /// <summary>
        /// 
        /// </summary>
        public readonly JobGroupInfo Owner;

        /// <summary>
        /// 
        /// </summary>
        public int Id => _source.Id;

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<JobGroupInfo> ChildGroups { get; set; }

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
        /// <param name="owner"></param>
        /// <param name="source"></param>
        public JobGroupInfo(JobGroupInfo owner, JobGroup source)
        {
            Owner = owner;
            _source = source;

            var childJobGroups = source?.ChildGroups.Select(s => new JobGroupInfo(this, s)).ToList() ?? new List<JobGroupInfo>();
            ChildGroups = new ObservableCollection<JobGroupInfo>(childJobGroups);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public JobGroupInfo(JobGroup source)
        {
            _source = source;

            var childJobGroups = source?.ChildGroups.Select(s => new JobGroupInfo(s)).ToList() ?? new List<JobGroupInfo>();
            ChildGroups = new ObservableCollection<JobGroupInfo>(childJobGroups);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Submit()
        {
            JobsDatabase.JobGroups.Update(_source);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SubmitOwner()
        {
            Owner?.Submit();
        }
    }
}
