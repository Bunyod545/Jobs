using System.Diagnostics;
using System.Text;
using DotnetPublish.Tasks.Logics.Initializer;
using Jobs.Tasks.Common;
using Jobs.Tasks.Common.Logics.Initializer.Attributes;
using Jobs.Tasks.Common.Logics.Services.Log;

namespace DotnetPublish.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [TaskInitializer(typeof(PublishTaskInitializer))]
    public partial class PublishTask : ITask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITaskLogService _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public PublishTask(ITaskLogService log)
        {
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
            startInfo.Arguments = GetArguments();
            startInfo.CreateNoWindow = true;

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
        /// <returns></returns>
        private string GetArguments()
        {
            var argBuilder = new StringBuilder();
            argBuilder.Append($"/c dotnet publish {ProjectPath}");

            if (!string.IsNullOrEmpty(Configuration))
                argBuilder.Append($" -c {Configuration}");

            if (!string.IsNullOrEmpty(Framework))
                argBuilder.Append($" --framework {Framework}");

            if (!string.IsNullOrEmpty(Runtime))
                argBuilder.Append($" -r {Runtime}");

            return argBuilder.ToString();
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
            _log.Success("Publish success");
            _log.Information(string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowError()
        {
            _log.Information(string.Empty);
            _log.Error("Publish failed");
            _log.Information(string.Empty);
        }
    }
}
