using System.Windows;

namespace Test.Tasks.Views
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TestTaskDataEditor
    {
        /// <summary>
        /// 
        /// </summary>
        public TestTaskDataEditorModel Model { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public TestTaskDataEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestTaskDataEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            Model = GetTaskData<TestTaskDataEditorModel>() ?? new TestTaskDataEditorModel();
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
