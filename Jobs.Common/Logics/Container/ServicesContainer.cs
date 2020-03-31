using System;
using System.Threading;
using Autofac;

namespace Jobs.Common.Logics.Container
{
    /// <summary>
    /// 
    /// </summary>
    public class ServicesContainer
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly AsyncLocal<ServicesContainer> CurrentAsyncLocal = new AsyncLocal<ServicesContainer>();

        /// <summary>
        /// 
        /// </summary>
        public static ServicesContainer Current => CurrentAsyncLocal.Value;

        /// <summary>
        /// 
        /// </summary>
        public ContainerBuilder Builder { get; }

        /// <summary>
        /// 
        /// </summary>
        public IContainer Container { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal ServicesContainer()
        {
            CurrentAsyncLocal.Value = this;
            Builder = new ContainerBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        public void Register<TInterface>(TInterface implementation) where TInterface : class
        {
            Builder.RegisterInstance(implementation).As<TInterface>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Builder.RegisterType<TImplementation>().As<TInterface>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            Container = Builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetService(Type type)
        {
            return Container.Resolve(type);
        }
    }
}
