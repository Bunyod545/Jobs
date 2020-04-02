using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Jobs.Tasks.Common;
using Jobs.Tasks.Common.Logics.Services.Log;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace SystemctlService.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public abstract partial class SystemctlBaseTask
    {
        /// <summary>
        /// 
        /// </summary>
        public string SshHost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SshLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SshPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }
    }
}
