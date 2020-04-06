using System.Windows;
using Jobs.Tasks.Common.Helpers;

namespace Sftp.Tasks.Views
{
    /// <summary>
    ///
    /// </summary>
    public partial class SftpFolderBackupTaskEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public SftpFolderBackupTaskEditorModel ViewModel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public SftpFolderBackupTaskEditor()
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
            ViewModel = GetTaskData<SftpFolderBackupTaskEditorModel>() ?? new SftpFolderBackupTaskEditorModel();
            DataContext = ViewModel;
            PasswordBox.Password = PasswordHelper.Decrypt(ViewModel.SftpPassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBox.Password))
                ViewModel.SftpPassword = PasswordHelper.Encrypt(PasswordBox.Password);

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
