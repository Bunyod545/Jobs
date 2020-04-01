namespace Jobs.Tasks.Common.Logics.Services.Log
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskLogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Success(string msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Information(string msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Warning(string msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Error(string msg);
    }
}
