using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Jobs.Tasks.Common;
using Jobs.Tasks.Common.Logics.Services.Log;
using Renci.SshNet;

namespace SystemctlService.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SystemctlBaseTask : ITask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskLogService _log;

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
        public string ServiceName { get; set; }

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
            ExpectSSH(SftpHost, SftpLogin, SftpPassword);
            return true;
            var client = new SshClient(SftpHost, SftpLogin, SftpPassword);
            client.Connect();

            var shellStream = client.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
            SwithToRoot(SftpPassword, shellStream);
            WriteStream($"systemctl {GetCommandName()} {ServiceName}", shellStream);

            var result = ReadStream(shellStream);
            client.Disconnect();

            return true;
        }

        public void ExpectSSH(string address, string login, string password)
        {
            try
            {
                SshClient sshClient = new SshClient(address, 22, login, password);

                sshClient.Connect();
                IDictionary<Renci.SshNet.Common.TerminalModes, uint> termkvp = new Dictionary<Renci.SshNet.Common.TerminalModes, uint>();
                termkvp.Add(Renci.SshNet.Common.TerminalModes.ECHO, 53);

                ShellStream shellStream = sshClient.CreateShellStream("xterm", 80, 24, 800, 600, 1024, termkvp);


                //Get logged in
                string rep = shellStream.Expect(new Regex(@"[$>]")); //expect user prompt

                //send command
                shellStream.WriteLine("sudo su");
                rep = shellStream.Expect(new Regex(@"([$#>:])")); //expect password or user prompt

                //check to send password
                if (rep.Contains(":"))
                {
                    //send password
                    shellStream.WriteLine(password);
                    rep = shellStream.Expect(new Regex(@"[$#>]")); //expect user or root prompt
                }

                sshClient.Disconnect();
            }//try to open connection
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="stream"></param>
        private static void WriteStream(string cmd, ShellStream stream)
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
        private static string ReadStream(ShellStream stream)
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
        private static void SwithToRoot(string password, ShellStream stream)
        {
            var prompt = stream.Expect(new Regex(@"[$>]"));

            stream.WriteLine("sudo su");
            prompt = stream.Expect(new Regex(@"([$#>:])"));

            if (!prompt.Contains(":"))
                return;

            stream.WriteLine(password);
            var str = ReadStream(stream);
            prompt = stream.Expect(new Regex(@"[$#>]"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetCommandName()
        {
            return string.Empty;
        }
    }
}
