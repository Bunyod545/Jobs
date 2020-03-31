using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Jobs.Tasks.Common;
using Jobs.Tasks.Common.Logics.Services.Log;

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
            var directory = GetAssemblyDirectory();
            var plink = Path.Combine(directory, "plink.exe");

            var process = new Process();
            process.EnableRaisingEvents = true;

            var startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = plink;
            startInfo.Arguments = " -ssh uzcard_admin@172.17.8.132 -pw \"6g2t&-N$-A*z+py\"";
           // startInfo.CreateNoWindow = true;

            process.StartInfo = startInfo;
            process.OutputDataReceived += Cmd_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            var result = process.ExitCode == 0;
            if (result)
                _log.Information("Publish success");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            _log.Information(e.Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetCommandName()
        {
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }
    }
}
