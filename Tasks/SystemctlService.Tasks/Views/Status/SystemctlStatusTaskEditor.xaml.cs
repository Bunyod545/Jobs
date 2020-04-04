using System.Windows;
using Jobs.Tasks.Common.Helpers;

namespace SystemctlService.Tasks.Views.Status
{
    /// <summary>
    ///
    /// </summary>
    public partial class SystemctlStatusTaskEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public SystemctlStatusTaskEditorModel ViewModel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public SystemctlStatusTaskEditor()
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
            ViewModel = GetTaskData<SystemctlStatusTaskEditorModel>() ?? new SystemctlStatusTaskEditorModel();
            DataContext = ViewModel;
            PasswordBox.Password = PasswordHelper.Decrypt(ViewModel.SshPassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBox.Password))
                ViewModel.SshPassword = PasswordHelper.Encrypt(PasswordBox.Password);

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
