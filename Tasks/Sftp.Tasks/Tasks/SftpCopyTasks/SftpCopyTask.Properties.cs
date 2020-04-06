using Jobs.Tasks.Common.Logics.DataEditor.Attributes;
using Sftp.Tasks.Views;

namespace Sftp.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(SftpCopyTaskEditor))]
    public partial class SftpCopyTask
    {
        /// <summary>
        /// 
        /// </summary>
        public string SftpHost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SftpLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SftpPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FromPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ToPath { get; set; }
    }
}
