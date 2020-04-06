using Renci.SshNet;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Jobs.Tasks.Common.Helpers;
using Jobs.Tasks.Common.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Tasks;

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
            var decryptedPassword = PasswordHelper.Decrypt(SftpPassword);
            var sftpClient = new SftpClient(SftpHost, SftpLogin, decryptedPassword);

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
            var directoryInfo = new DirectoryInfo(FromPath);
            return directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
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
            infos.ForEach(f => CopyByFileSystemInfo(client, f, remotePath));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="info"></param>
        /// <param name="remotePath"></param>
        private void CopyByFileSystemInfo(SftpClient client, FileSystemInfo info, string remotePath)
        {
            if (info.Attributes.HasFlag(FileAttributes.Directory))
            {
                CopyDirectory(client, info, remotePath);
                return;
            }

            CopyFile(client, info, remotePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="info"></param>
        /// <param name="remotePath"></param>
        private void CopyDirectory(SftpClient client, FileSystemInfo info, string remotePath)
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
        /// <param name="remotePath"></param>
        private void CopyFile(SftpClient client, FileSystemInfo info, string remotePath)
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

            var totalMb = BytesToMb(_filesTotalSize);
            var needCopyMb = BytesToMb(_filesCopiedSize);
            var copiedMb = totalMb - needCopyMb;

            _paragraph.Inlines.Clear();
            _paragraph.Inlines.Add($"Files copy {copiedMb} mb/{totalMb} mb");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private long BytesToMb(long bytes)
        {
            return bytes / 1024 / 1024;
        }
    }
}
