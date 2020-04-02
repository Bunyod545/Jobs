namespace Jobs.Manager.Views.Tasks.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EditTaskComponent
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly EditTaskComponentModel ViewModel;

        /// <summary>
        /// 
        /// </summary>
        public EditTaskComponent()
        {
            InitializeComponent();
            ViewModel = new EditTaskComponentModel();
            DataContext = ViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        public EditTaskComponent(EditTaskComponentModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }
    }
}
