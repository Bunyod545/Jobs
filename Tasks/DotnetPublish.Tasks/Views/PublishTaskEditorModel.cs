using Jobs.Tasks.Common.Models;

namespace DotnetPublish.Tasks.Views
{
    /// <summary>
    /// 
    /// </summary>
    public class PublishTaskEditorModel : BaseViewModel
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
