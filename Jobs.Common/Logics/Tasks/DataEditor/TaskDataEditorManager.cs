using System.Reflection;
using Jobs.Common.Database.Tables;
using Jobs.Common.Helpers;
using Jobs.Common.Logics.Tasks.Finder;
using Jobs.Tasks.Common.Logics.DataEditor;
using Jobs.Tasks.Common.Logics.DataEditor.Attributes;

namespace Jobs.Common.Logics.Tasks.DataEditor
{
    /// <summary>
    /// 
    /// </summary>
    public static class TaskDataEditorManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static BaseTaskDataEditor GetDataEditor(Task task)
        {
            var type = TaskFinder.FindType(task);
            var attr = type.GetCustomAttribute<TaskDataEditorAttribute>();
            if (attr == null)
                return null;

            return ActivatorHelper.CreateInstance(attr.ViewType) as BaseTaskDataEditor;
        }
    }
}
