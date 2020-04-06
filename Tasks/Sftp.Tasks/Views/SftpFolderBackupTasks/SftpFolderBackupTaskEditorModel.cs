using Jobs.Tasks.Common.Models;

namespace Sftp.Tasks.Views
{
    /// <summary>
    /// 
    /// </summary>
    public class SftpFolderBackupTaskEditorModel : BaseViewModel
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
