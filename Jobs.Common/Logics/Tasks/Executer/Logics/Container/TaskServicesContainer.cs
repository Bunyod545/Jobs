using System;
using Jobs.Common.Logics.Container;
using Jobs.Tasks.Common.Logics.Services.Container;

namespace Jobs.Common.Logics.Tasks.Logics.Container
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskServicesContainer : ITaskServicesContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return ServicesContainer.Current.GetService<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetService(Type type)
        {
            return ServicesContainer.Current.GetService(type);
        }
    }
}