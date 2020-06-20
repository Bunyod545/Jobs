using System.Diagnostics;
using System.Globalization;
using System.Text;
using Jobs.Tasks.Common.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Tasks;

namespace Cmd.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CmdTask : ITask
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
        /// <param name="customizedLog"></param>
        /// <param name="log"></param>
        public CmdTask(ITaskCustomizedLogService customizedLog, ITaskLogService log)
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
            var process = new Process();
            process.EnableRaisingEvents = true;

            var startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments ="/C " + CmdText;
            startInfo.CreateNoWindow = true;
            startInfo.StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);

            process.StartInfo = startInfo;
            process.OutputDataReceived += Cmd_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            var result = process.ExitCode == 0;
            if (result)
            {
                ShowSuccess();
                return true;
            }

            ShowError();
            return false;
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
        private void ShowSuccess()
        {
            _log.Information(string.Empty);
            _log.Success("Cmd execute success");
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowError()
        {
            _log.Information(string.Empty);
            _log.Error("Cmd execute failed");
        }
    }
}
