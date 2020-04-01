namespace Sftp.Tasks
{
    /// <summary>
    /// 
    /// </summary>
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
