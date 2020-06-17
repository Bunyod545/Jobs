using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Jobs.Tasks.Common.Helpers;
using Jobs.Tasks.Common.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace Sftp.Tasks.Tasks.SftpFolderBackupTasks
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SftpFolderBackupTask : ITask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskLogService _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public SftpFolderBackupTask(ITaskLogService log)
        {
            _log = log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            var decryptedPassword = EncrytionHelper.Decrypt(SftpPassword);
            var client = new SshClient(SftpHost, SftpLogin, decryptedPassword);
            client.Connect();
            var termkvp = new Dictionary<TerminalModes, uint>();
            termkvp.Add(TerminalModes.ECHO, 53);

            _log.Information("Execute FolderBackupTask begin");
            var shellStream = client.CreateShellStream("xterm", 80, 24, 800, 600, 1024, termkvp);

            SwithToRoot(decryptedPassword, shellStream);
            var backupfolder = GetBackupFolderPath();
          
            WriteStream($"mkdir {backupfolder}", shellStream);
            var result = ReadStream(shellStream);
            _log.Information(result);

            WriteStream($"cp -r {FromPath}/* {backupfolder}", shellStream);
            result = ReadStream(shellStream);
           
            _log.Information(result);
            _log.Success("Execute FolderBackupTask finished!");
            
            client.Disconnect();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="stream"></param>
        private void SwithToRoot(string password, ShellStream stream)
        {
            var prompt = stream.Expect(new Regex(@"[$>]"));
            _log.Information(prompt);

            stream.WriteLine("sudo su");
            prompt = stream.Expect(new Regex(@"([$#>:])"));
            _log.Information(prompt);

            if (!prompt.Contains(":"))
                return;

            stream.WriteLine(password);
            prompt = stream.Expect(new Regex(@"[$#>]"));
            _log.Information(prompt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetBackupFolderPath()
        {
            var fromPathName = FromPath.Substring(FromPath.LastIndexOf('/'));
            return BackupPath + fromPathName + $"_{DateTime.Now:dd_MM_yyyy_hh_mm_ss}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="stream"></param>
        private void WriteStream(string cmd, ShellStream stream)
        {
            stream.WriteLine(cmd + "; echo this-is-the-end");
            while (stream.Length == 0)
                Thread.Sleep(500);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private string ReadStream(ShellStream stream)
        {
            var result = new StringBuilder();

            string line;
            while ((line = stream.ReadLine()) != "this-is-the-end")
                result.AppendLine(line);

            return result.ToString();
        }

    }
}
