using Jobs.Tasks.Common.Logics.Initializer.Models;

namespace Jobs.Tasks.Common.Logics.Initializer
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInitializeOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public ITaskServicesRegistrator Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Container.Register<TInterface, TImplementation>();
        }
    }
}