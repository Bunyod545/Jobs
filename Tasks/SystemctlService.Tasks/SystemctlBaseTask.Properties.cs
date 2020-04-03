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
