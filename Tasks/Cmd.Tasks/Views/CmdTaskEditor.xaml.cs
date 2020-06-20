using System.Windows;

namespace Cmd.Tasks.Views
{
    /// <summary>
    ///
    /// </summary>
    public partial class CmdTaskEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public CmdTaskEditorModel ViewModel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CmdTaskEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SftpCopyTaskEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel = GetTaskData<CmdTaskEditorModel>() ?? new CmdTaskEditorModel();
            DataContext = ViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SetTaskData(ViewModel);
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
