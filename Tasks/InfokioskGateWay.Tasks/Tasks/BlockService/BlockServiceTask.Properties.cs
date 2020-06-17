using InfokioskGateWay.Tasks.Views.BlockService;
using Jobs.Tasks.Common.Logics.DataEditor.Attributes;

namespace InfokioskGateWay.Tasks.Tasks.BlockService
{
    /// <summary>
    /// 
    /// </summary>
    [TaskDataEditor(typeof(BlockServiceTaskDataEditor))]
    public partial class BlockServiceTask
    {
        /// <summary>
        /// 
        /// </summary>
        public string BlockServiceUrl { get; set; }
    }
}
