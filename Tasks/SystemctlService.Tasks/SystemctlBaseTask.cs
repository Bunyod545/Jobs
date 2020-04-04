using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Jobs.Tasks.Common;
using Jobs.Tasks.Common.Helpers;
using Jobs.Tasks.Common.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace SystemctlService.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public abstract partial class SystemctlBaseTask : ITask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskLogService _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        protected SystemctlBaseTask(ITaskLogService log)
        {
            _log = log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool Execute()
        {
            var decryptedPassword = PasswordHelper.Decrypt(SshPassword);
            var client = new SshClient(SshHost, SshLogin, decryptedPassword);
            client.Connect();

            var termkvp = new Dictionary<TerminalModes, uint>();
            termkvp.Add(TerminalModes.ECHO, 53);

            var shellStream = client.CreateShellStream("xterm", 80, 24, 800, 600, 1024, termkvp);
            SwithToRoot(decryptedPassword, shellStream);
            WriteStream($"systemctl {GetCommandName()} {ServiceName} -l", shellStream);

            var result = ReadStream(shellStream);
            _log.Information(result);

            if(IsWaitStatusChange())
                WaitStatusChange(shellStream);

            _log.Success("Execute systemctl finished!");
            _log.Information(string.Empty);
            _log.Information(string.Empty);

            client.Disconnect();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsWaitStatusChange()
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shellStream"></param>
        protected virtual void WaitStatusChange(ShellStream shellStream)
        {
            _log.Information("Waiting status!");
            string newText;

            while (true)
            {
                Thread.Sleep(1000);

                WriteStream($"systemctl status {ServiceName} -l", shellStream);
                newText = ReadStream(shellStream);

                if(newText.Contains("Press Ctrl+C to shut down"))
                    break;

                if (newText.Contains("entered failed state"))
                    break;
            }

            _log.Information(newText);
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
        public abstract string GetCommandName();
    }
}
