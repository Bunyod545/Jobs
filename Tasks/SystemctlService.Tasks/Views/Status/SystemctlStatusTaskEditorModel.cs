using Jobs.Tasks.Common.Models;

namespace SystemctlService.Tasks.Views.Status
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemctlStatusTaskEditorModel : BaseViewModel
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
