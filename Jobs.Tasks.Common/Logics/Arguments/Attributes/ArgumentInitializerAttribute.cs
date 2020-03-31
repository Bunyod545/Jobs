using System;

namespace Jobs.Tasks.Common.Logics.Arguments.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class ArgumentInitializerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public Type ArgumentInitializerType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentInitializerType"></param>
        public ArgumentInitializerAttribute(Type argumentInitializerType)
        {
            ArgumentInitializerType = argumentInitializerType;
        }
    }
}