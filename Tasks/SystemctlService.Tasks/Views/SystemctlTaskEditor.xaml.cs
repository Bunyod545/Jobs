﻿using System.Windows;
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