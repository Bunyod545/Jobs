using Jobs.Common.Logics.Container;
using Jobs.Tasks.Common.Logics.Initializer.Models;
using Jobs.Tasks.Common.Logics.Services.Container;

namespace Jobs.Common.Logics.Tasks.Logics.Container
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskServicesRegistrator : ITaskServicesRegistrator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            ServicesContainer.Current.Register<TInterface, TImplementation>();
        }
    }
}
