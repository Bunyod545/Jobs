using System;
using System.Windows;
using System.Windows.Input;
using Jobs.Manager.Views.Jobs.Models;

namespace Jobs.Manager.Views.Jobs
{
    /// <summary>
    ///
    /// </summary>
    public partial class JobAddView
    {
        /// <summary>
        /// 
        /// </summary>
        public JobInfo Model => DataContext as JobInfo;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<JobInfo> Delete;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<JobInfo> ComponentClick;

        /// <summary>
        /// 
        /// </summary>
        public JobAddView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Model.IsEdit)
                Model.Update();

            Model.IsEdit = !Model.IsEdit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(this, Model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ComponentClick?.Invoke(this, Model);
        }
    }
}
