using System;

namespace Jobs.Tasks.Common.Logics.DataEditor.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskDataEditorAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public Type ViewType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewType"></param>
        public TaskDataEditorAttribute(Type viewType)
        {
            ViewType = viewType;
        }
    }
}