using System.Windows;
using System.Windows.Forms;
using Sftp.Tasks.Helpers;

namespace Sftp.Tasks.Views
{
    /// <summary>
    ///
    /// </summary>
    public partial class SftpCopyTaskEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public SftpCopyTaskEditorModel ViewModel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public SftpCopyTaskEditor()
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
            ViewModel = GetTaskData<SftpCopyTaskEditorModel>() ?? new SftpCopyTaskEditorModel();
            DataContext = ViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FromBaseButton_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            ViewModel.FromPath = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBox.Password))
                ViewModel.SftpPassword = SftpCopyPasswordHelper.Encrypt(PasswordBox.Password);

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
