using System.Windows;
using Microsoft.Win32;

namespace DotnetPublish.Tasks.Views
{
    /// <summary>
    ///
    /// </summary>
    public partial class PublishTaskEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public PublishTaskEditorModel ViewModel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public PublishTaskEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PublishTaskEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel = GetTaskData<PublishTaskEditorModel>() ?? new PublishTaskEditorModel();
            DataContext = ViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSharp project file | *.csproj";
            fileDialog.ShowDialog(this);

            ViewModel.ProjectPath = fileDialog.FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PublishProfileBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Publish profile file | *.pubxml";
            fileDialog.ShowDialog(this);

            ViewModel.PublishProfilePath = fileDialog.FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SetTaskData(ViewModel);
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
