using DotnetPublish.Tasks.Views;
using Jobs.Tasks.Common.Logics.DataEditor.Attributes;

namespace DotnetPublish.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(PublishTaskEditor))]
    public partial class PublishTask
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProjectPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PublishProfilePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Framework { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Runtime { get; set; }
    }
}
