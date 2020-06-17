using InfokioskGateWay.Tasks.Views.UnlockService;
using Jobs.Tasks.Common.Logics.DataEditor.Attributes;

namespace InfokioskGateWay.Tasks.Tasks.UnlockService
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(UnlockServiceTaskDataEditor))]
    public partial class UnlockServiceTask
    {
        /// <summary>
        /// 
        /// </summary>
        public string UnlockServiceUrl { get; set; }
    }
}
