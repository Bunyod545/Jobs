using Cmd.Tasks.Views;
using Jobs.Tasks.Common.Logics.DataEditor.Attributes;

namespace Cmd.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(CmdTaskEditor))]
    public partial class CmdTask
    {
        /// <summary>
        /// 
        /// </summary>
        public string CmdText { get; set; }
    }
}
