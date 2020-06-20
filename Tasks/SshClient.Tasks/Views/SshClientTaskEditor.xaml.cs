using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemctlService.Tasks.Logics.HostData;
using Jobs.Tasks.Common.Helpers;
using SshClient.Tasks.Logics.HostData.Models;

namespace SshClient.Tasks.Views
{
    /// <summary>
    ///
    /// </summary>
    public partial class SshClientTaskEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public SshClientTaskEditorModel ViewModel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public SshClientTaskEditor()
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
            ViewModel = GetTaskData<SshClientTaskEditorModel>() ?? new SshClientTaskEditorModel();

            HostsCombobox.Text = ViewModel.SshHost;
            LoginsCombobox.Text = ViewModel.SshLogin;
            PasswordBox.Password = EncrytionHelper.Decrypt(ViewModel.SshPassword);
            CmdTextBox.Text = ViewModel.Command;
            IsRootCheckBox.IsChecked = ViewModel.IsRoot;

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
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var hostInfo = new HostSubmitInfo();
            hostInfo.Host = HostsCombobox.Text;
            hostInfo.Login = LoginsCombobox.Text;
            hostInfo.Password = PasswordBox.Password;
            HostDataManager.SubmitHostInfo(hostInfo);

            ViewModel.SshHost = HostsCombobox.Text;
            ViewModel.SshLogin = LoginsCombobox.Text;
            ViewModel.Command = CmdTextBox.Text;
            ViewModel.IsRoot = IsRootCheckBox.IsChecked.GetValueOrDefault();
            
            if (!string.IsNullOrEmpty(PasswordBox.Password))
                ViewModel.SshPassword = EncrytionHelper.Encrypt(PasswordBox.Password);

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
