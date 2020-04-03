namespace Jobs.Manager.Views.Tasks.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TaskItemComponent
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly TaskItemComponentModel ViewModel;

        /// <summary>
        /// 
        /// </summary>
        public TaskItemComponent()
        {
            InitializeComponent();
            ViewModel = new TaskItemComponentModel();
            DataContext = ViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        public TaskItemComponent(TaskItemComponentModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }
    }
}
