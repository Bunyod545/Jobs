﻿using System.Collections.Generic;

namespace SshClient.Tasks.Logics.HostData.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class HostInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<HostLoginInfo> LoginInfos { get; set; }
    }
}
