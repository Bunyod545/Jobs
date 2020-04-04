using Jobs.Tasks.Common.Models;

namespace SystemctlService.Tasks.Views
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemctlTaskEditorModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string SshHost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SshLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SshPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }
    }
}
