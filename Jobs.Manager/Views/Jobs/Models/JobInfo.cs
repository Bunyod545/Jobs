using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Jobs.Common.Database;
using Jobs.Common.Database.Tables;
using Jobs.Manager.Views.Tasks.Models;
using Jobs.Tasks.Common.Models;

namespace Jobs.Manager.Views.Jobs.Models
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
        public ObservableCollection<TaskInfo> Tasks { get; set; }

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

            var sourceTasks = source.Tasks?.Select(s => new TaskInfo(this, s)).ToList() ?? new List<TaskInfo>();
            Tasks = new ObservableCollection<TaskInfo>(sourceTasks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskInfo"></param>
        public void AddTask(TaskInfo taskInfo)
        {
            Tasks.Add(taskInfo);
            Source.Tasks.Add(taskInfo.Source);
            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskInfo"></param>
        public void DeleteTask(TaskInfo taskInfo)
        {
            Tasks.Remove(taskInfo);
            Source.Tasks.Remove(taskInfo.Source);
            Update();
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
