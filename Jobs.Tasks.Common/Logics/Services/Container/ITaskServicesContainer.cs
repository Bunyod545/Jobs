using System;

namespace Jobs.Tasks.Common.Logics.Services.Container
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskServicesContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetService<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetService(Type type);
    }
}