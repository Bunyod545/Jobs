using Jobs.Tasks.Common;
using Renci.SshNet;
using System.IO;
using System.Linq;

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
        /// <returns></returns>
        public bool Execute()
        {
            var sftpClient = new SftpClient(SftpHost, SftpLogin, SftpPassword);
            sftpClient.Connect();
            UploadDirectory(sftpClient, FromPath, ToPath);

            sftpClient.Disconnect();
            sftpClient.Dispose();
            return true;
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
        }
    }
}
