using Jobs.Tasks.Common.Logics.DataEditor.Attributes;
using Test.Tasks.Views;

namespace Test.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(TestTaskDataEditor))]
    public partial class TestTask
    {
        /// <summary>
        /// 
        /// </summary>
       public string Name { get; set; }
    }
}
