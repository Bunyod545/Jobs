namespace Jobs.Tasks.Common.Logics.Initializer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        void Initialize(TaskInitializeOptions options);
    }
}