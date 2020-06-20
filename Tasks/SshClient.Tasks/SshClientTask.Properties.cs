using Jobs.Tasks.Common.Logics.DataEditor.Attributes;
using SshClient.Tasks.Views;

namespace SshClient.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(SshClientTaskEditor))]
    public partial class SshClientTask
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
