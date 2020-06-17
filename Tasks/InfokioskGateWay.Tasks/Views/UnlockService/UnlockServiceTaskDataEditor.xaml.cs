using System.Windows;

namespace InfokioskGateWay.Tasks.Views.UnlockService
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UnlockServiceTaskDataEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public UnlockServiceTaskDataEditorModel Model { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public UnlockServiceTaskDataEditor()
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
            Model = GetTaskData<UnlockServiceTaskDataEditorModel>() ?? new UnlockServiceTaskDataEditorModel();
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
