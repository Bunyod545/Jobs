using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemctlService.Tasks.Logics.HostData;
using SystemctlService.Tasks.Logics.HostData.Models;
using Jobs.Tasks.Common.Helpers;

namespace SystemctlService.Tasks.Views
{
    /// <summary>
    ///
    /// </summary>
    public partial class SystemctlTaskEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public SystemctlTaskEditorModel ViewModel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public SystemctlTaskEditor()
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
            ViewModel = GetTaskData<SystemctlTaskEditorModel>() ?? new SystemctlTaskEditorModel();

            HostsCombobox.Text = ViewModel.SshHost;
            LoginsCombobox.Text = ViewModel.SshLogin;
            PasswordBox.Password = EncrytionHelper.Decrypt(ViewModel.SshPassword);
            ServiceTextBox.Text = ViewModel.ServiceName;

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
            ViewModel.ServiceName = ServiceTextBox.Text;

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
