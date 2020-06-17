using System.Windows;

namespace InfokioskGateWay.Tasks.Views.BlockService
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BlockServiceTaskDataEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public BlockServiceTaskDataEditorModel Model { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public BlockServiceTaskDataEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskDataEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            Model = GetTaskData<BlockServiceTaskDataEditorModel>() ?? new BlockServiceTaskDataEditorModel();
            DataContext = Model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SetTaskData(Model);
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
