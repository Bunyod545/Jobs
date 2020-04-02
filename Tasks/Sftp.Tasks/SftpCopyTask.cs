using Jobs.Tasks.Common;
using Renci.SshNet;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Jobs.Tasks.Common.Logics.Services.Log;

namespace Sftp.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SftpCopyTask : ITask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskCustomizedLogService _customizedLog;

        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskLogService _log;

        /// <summary>
        /// 
        /// </summary>
        private long _filesTotalSize;

        /// <summary>
        /// 
        /// </summary>
        private long _filesCopiedSize;

        /// <summary>
        /// 
        /// </summary>
        private Paragraph _paragraph;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customizedLog"></param>
        /// <param name="log"></param>
        public SftpCopyTask(ITaskCustomizedLogService customizedLog, ITaskLogService log)
        {
            _customizedLog = customizedLog;
            _log = log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            var sftpClient = new SftpClient(SftpHost, SftpLogin, SftpPassword);
            sftpClient.Connect();
            _log.Information("Files copy begin");

            _filesTotalSize = GetTotalSize();
            _filesCopiedSize = _filesTotalSize;

            UploadDirectory(sftpClient, FromPath, ToPath);
            sftpClient.Disconnect();
            sftpClient.Dispose();

            _log.Success("Files copy success");
            _log.Information(string.Empty);
            _log.Information(string.Empty);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private long GetTotalSize()
        {
            var di = new DirectoryInfo(FromPath);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="localPath"></param>
        /// <param name="remotePath"></param>
        private void UploadDirectory(SftpClient client, string localPath, string remotePath)
        {
            var infos = new DirectoryInfo(localPath).EnumerateFileSystemInfos().ToList();
            infos.ForEach(f => CopyByFileSystemInfo(client, f, localPath, remotePath));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="info"></param>
        /// <param name="localPath"></param>
        /// <param name="remotePath"></param>
        private void CopyByFileSystemInfo(SftpClient client, FileSystemInfo info, string localPath, string remotePath)
        {
            if (info.Attributes.HasFlag(FileAttributes.Directory))
            {
                CopyDirectory(client, info, localPath, remotePath);
                return;
            }

            CopyFile(client, info, localPath, remotePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="info"></param>
        /// <param name="localPath"></param>
        /// <param name="remotePath"></param>
        private void CopyDirectory(SftpClient client, FileSystemInfo info, string localPath, string remotePath)
        {
            var subPath = remotePath + "/" + info.Name;
            if (!client.Exists(subPath))
                client.CreateDirectory(subPath);

            UploadDirectory(client, info.FullName, remotePath + "/" + info.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="info"></param>
        /// <param name="localPath"></param>
        /// <param name="remotePath"></param>
        private void CopyFile(SftpClient client, FileSystemInfo info, string localPath, string remotePath)
        {
            using (Stream fileStream = new FileStream(info.FullName, FileMode.Open))
                client.UploadFile(fileStream, remotePath + "/" + info.Name, true);

            var fileIno = new FileInfo(info.FullName);
            _filesCopiedSize -= fileIno.Length;
            ShowLog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowLog()
        {
            _customizedLog.Show(ShowLogOnUiThread);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        private void ShowLogOnUiThread(RichTextBox richTextBox)
        {
            if (_paragraph == null)
            {
                _paragraph = new Paragraph();
                _paragraph.Margin = new Thickness(0, 0, 0, 2);
                _paragraph.Foreground = new SolidColorBrush(Color.FromRgb(116, 220, 251));

                richTextBox.Document.Blocks.Add(_paragraph);
                richTextBox.ScrollToEnd();
            }

            var totalMb = _filesTotalSize / 1024 / 1024;
            var needCopyMb = _filesCopiedSize / 1024 / 1024;
            var copiedMb = totalMb - needCopyMb;

            _paragraph.Inlines.Clear();
            _paragraph.Inlines.Add($"Files copy {copiedMb} mb/{totalMb} mb");
        }
    }
}
