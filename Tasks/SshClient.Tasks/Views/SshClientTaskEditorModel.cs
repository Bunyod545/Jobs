using Jobs.Tasks.Common.Models;

namespace SshClient.Tasks.Views
{
    /// <summary>
    /// 
    /// </summary>
    public class SshClientTaskEditorModel : BaseViewModel
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
        public string Command { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRoot { get; set; }
    }
}
