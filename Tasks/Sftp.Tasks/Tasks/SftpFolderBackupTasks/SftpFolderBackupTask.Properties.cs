using Jobs.Tasks.Common.Logics.DataEditor.Attributes;
using Sftp.Tasks.Views;

namespace Sftp.Tasks.Tasks.SftpFolderBackupTasks
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(SftpFolderBackupTaskEditor))]
    public partial class SftpFolderBackupTask 
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
        public string BackupPath { get; set; }
    }
}
