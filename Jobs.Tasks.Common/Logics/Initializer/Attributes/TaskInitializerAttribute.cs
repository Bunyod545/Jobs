using System;

namespace Jobs.Tasks.Common.Logics.Initializer.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskInitializerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public Type InitalizerType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initalizerType"></param>
        public TaskInitializerAttribute(Type initalizerType)
        {
            InitalizerType = initalizerType;
        }
    }
}
