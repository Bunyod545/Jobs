using System;
using System.Linq;
using Jobs.Common.Logics.Container;

namespace Jobs.Common.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActivatorHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            return (T) CreateInstance(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CreateInstance(Type type)
        {
            var constructorInfo = type.GetConstructors().FirstOrDefault();
            var constructorParamsInfo = constructorInfo.GetParameters();
            var constructorParams = new object[constructorParamsInfo.Length];

            for (var i = 0; i < constructorParamsInfo.Length; i++)
            {
                var parameterInfo = constructorParamsInfo[i];
                constructorParams[i] = ServicesContainer.Current.GetService(parameterInfo.ParameterType);
            }

            return Activator.CreateInstance(type, constructorParams);
        }
    }
}
