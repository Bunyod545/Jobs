namespace Jobs.Tasks.Common.Logics.Initializer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskServicesRegistrator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
    }
}
