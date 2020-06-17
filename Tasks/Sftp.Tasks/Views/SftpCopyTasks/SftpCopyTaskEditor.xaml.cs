using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using SystemctlService.Tasks.Logics.HostData;
using Jobs.Tasks.Common.Helpers;
using Sftp.Tasks.Logics.HostData.Models;

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

            HostsCombobox.Text = ViewModel.SftpHost;
            LoginsCombobox.Text = ViewModel.SftpLogin;
            PasswordBox.Password = EncrytionHelper.Decrypt(ViewModel.SftpPassword);
            FromPathTextBox.Text = ViewModel.FromPath;
            ToPathTextBox.Text = ViewModel.ToPath;

            var hosts = HostDataManager.GetHostsInfos().Select(s => s.Host).ToList();
            hosts.ForEach(f => HostsCombobox.Items.Add(f));

            HostsCombobox.SelectionChanged += Hosts_OnSelectionChanged;
            LoginsCombobox.SelectionChanged += Logins_OnSelectionChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hosts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var hostInfos = HostDataManager.GetHostInfo(HostsCombobox.SelectedItem?.ToString());
            var logins = hostInfos.LoginInfos.Select(s => s.Login).ToList();

            LoginsCombobox.Items.Clear();
            logins.ForEach(f => LoginsCombobox.Items.Add(f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logins_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var host = HostsCombobox.SelectedItem?.ToString();
            var login = LoginsCombobox.SelectedItem?.ToString();

            var hostInfo = HostDataManager.GetHostLoginInfo(host, login);
            if (hostInfo != null)
                PasswordBox.Password = hostInfo.Password;
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

            FromPathTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var hostInfo = new HostSubmitInfo();
            hostInfo.Host = HostsCombobox.Text;
            hostInfo.Login = LoginsCombobox.Text;
            hostInfo.Password = PasswordBox.Password;
            HostDataManager.SubmitHostInfo(hostInfo);

            ViewModel.SftpHost = HostsCombobox.Text;
            ViewModel.SftpLogin = LoginsCombobox.Text;
            ViewModel.FromPath = FromPathTextBox.Text;
            ViewModel.ToPath = ToPathTextBox.Text;

            if (!string.IsNullOrEmpty(PasswordBox.Password))
                ViewModel.SftpPassword = EncrytionHelper.Encrypt(PasswordBox.Password);

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
